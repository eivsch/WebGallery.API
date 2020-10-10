using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Factories;
using Infrastructure.Galleries;
using Infrastructure.Pictures;
using Infrastructure.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Domain.Tests
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public async Task TestOnlyVideo()
        {
            var generatorFactory = new GalleryGeneratorFactory(new TagRepositoryMock(), new PictureRepositoryMock(), new GalleryRepositoryMock());

            var galleryDescriptor = GalleryDescriptor.Create(1, "onlyvideos");

            var generator = generatorFactory.GetGalleryGenerator(galleryDescriptor);
            var gallery = await generator.GenerateGallery();

            Assert.IsTrue(gallery.GalleryItems.Count > 0);
        }

        [TestMethod]
        public async Task TestOnlyGifs()
        {
            var generatorFactory = new GalleryGeneratorFactory(new TagRepositoryMock(), new PictureRepositoryMock(), new GalleryRepositoryMock());

            var galleryDescriptor = GalleryDescriptor.Create(1, "onlygifs");

            var generator = generatorFactory.GetGalleryGenerator(galleryDescriptor);
            var gallery = await generator.GenerateGallery();

            Assert.IsTrue(gallery.GalleryItems.Count > 0);
        }

        [TestMethod]
        public async Task Test_AllRandom()
        {
            var generatorFactory = new GalleryGeneratorFactory(new TagRepositoryMock(), new PictureRepositoryMock(), new GalleryRepositoryMock());

            var galleryDescriptor = GalleryDescriptor.Create(10);

            var generator = generatorFactory.GetGalleryGenerator(galleryDescriptor);
            var gallery = await generator.GenerateGallery();

            Assert.IsTrue(gallery.GalleryItems.Count == 10);
        }
    }
}
