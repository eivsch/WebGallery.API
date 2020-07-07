using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class CustomExclusiveGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ITagRepository _tagRepository;

        public CustomExclusiveGenerator(IGalleryRepository galleryRepository, ITagRepository tagRepository, IPictureRepository pictureRepository)
            : base(pictureRepository)
        {
            _galleryRepository = galleryRepository;
            _tagRepository = tagRepository;
        }

        protected override async Task AddGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            var batch = await _galleryRepository.GetRandom(galleryDescriptor.NumberOfItems);
            foreach (var item in batch.GalleryItems)
            {
                if (gallery.GalleryItems.Count == galleryDescriptor.NumberOfItems)
                    break;

                var tags = await _tagRepository.FindAllTagsForPicture(item.Id);
                if (tags is null || tags.Count() == 0)
                    gallery.AddGalleryItem(item.Id, item.Index);
                else
                {
                    var matches = tags.Select(t => t.TagName).Intersect(galleryDescriptor.TagFilter.Tags);
                    if (matches.Count() == 0)
                        gallery.AddGalleryItem(item.Id, item.Index);
                }
            }
        }
    }
}
