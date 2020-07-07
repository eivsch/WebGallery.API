using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class AllRandomGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;

        public AllRandomGenerator(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            List<GeneratedItem> list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(galleryDescriptor.NumberOfItems);

            foreach (var item in batch.GalleryItems)
            {
                list.Add(new GeneratedItem
                {
                    Id = item.Id,
                    Index = item.Index
                });
            }

            return list;
        }
    }
}
