using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Tag.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class CustomExclusiveGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ITagRepository _tagRepository;

        public CustomExclusiveGenerator(IGalleryRepository galleryRepository, ITagRepository tagRepository)
        {
            _galleryRepository = galleryRepository;
            _tagRepository = tagRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            List<GeneratedItem> list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(galleryDescriptor.NumberOfItems);
            foreach (var item in batch.GalleryItems)
            {
                var tags = await _tagRepository.FindAllTagsForPicture(item.Id);
                if (tags is null || tags.Count() == 0)
                {
                    list.Add(new GeneratedItem
                    {
                        Id = item.Id,
                        Index = item.Index
                    });
                }
                else
                {
                    var matches = tags.Select(t => t.TagName).Intersect(galleryDescriptor.TagFilter.Tags);
                    if (matches.Count() == 0)
                    {
                        list.Add(new GeneratedItem
                        {
                            Id = item.Id,
                            Index = item.Index
                        });
                    }
                }
            }

            return list;
        }
    }
}
