using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public class GalleryCustomizerService : IGalleryCustomizerService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPictureRepository _pictureRepository;

        public GalleryCustomizerService(IGalleryRepository galleryRepository, ITagRepository tagRepository, IPictureRepository pictureRepository)
        {
            _galleryRepository = galleryRepository;
            _tagRepository = tagRepository;
            _pictureRepository = pictureRepository;
        }

        public async Task<Gallery> GetCustomizedGallery(Gallery gallery)
        {
            if (gallery.TagFilter == null)
                throw new ArgumentException("Gallery object is not valid - TagFilter is null");

            if (gallery.TagFilter.Mode == TagFilterMode.CustomInclusive)
            {
                while (gallery.GalleryItems.Count < gallery.NumberOfItems)
                {
                    var taggedImages = await _tagRepository.GetRandom(gallery.TagFilter.Tags, gallery.NumberOfItems);
                    await AddGalleryItems(gallery, taggedImages);
                }

                return gallery;
            }
            else if (gallery.TagFilter.Mode == TagFilterMode.OnlyTagged)
            {
                var taggedImages = await _tagRepository.GetRandom(null, gallery.NumberOfItems);
                await AddGalleryItems(gallery, taggedImages);

                return gallery;
            }
            else if (gallery.TagFilter.Mode == TagFilterMode.CustomExclusive)
            {
                while (gallery.GalleryItems.Count < gallery.NumberOfItems)
                {
                    var batch = await _galleryRepository.GetRandom(gallery.NumberOfItems);
                    foreach (var item in batch.GalleryItems)
                    {
                        if (gallery.GalleryItems.Count == gallery.NumberOfItems)
                            break;

                        var tags = await _tagRepository.FindAllTagsForPicture(item.Id);
                        if (tags is null || tags.Count() == 0)
                            gallery.AddGalleryItem(item.Id, item.Index);
                        else
                        {
                            var matches = tags.Select(t => t.TagName).Intersect(gallery.TagFilter.Tags);
                            if (matches.Count() == 0)
                                gallery.AddGalleryItem(item.Id, item.Index);
                        }
                    }
                }

                return gallery;
            }
            else    // Undefined
                return await _galleryRepository.GetRandom(gallery.NumberOfItems);
        }

        private async Task AddGalleryItems(Gallery gallery, IEnumerable<Tag> taggedImages)
        {
            foreach (var image in taggedImages)
            {
                if (gallery.GalleryItems.Count == gallery.NumberOfItems)
                    break;

                var picture = await _pictureRepository.FindById(image.PictureId);
                gallery.AddGalleryItem(picture.Id, picture.GlobalSortOrder);
            }
        }
    }
}
