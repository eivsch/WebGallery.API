using Application.Services;
using AutoMapper;
using DomainModel.Common.Enumerators;
using DomainModel.Factories;
using Infrastructure.Galleries;
using Infrastructure.Pictures;
using Infrastructure.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    [TestClass]
    public class GalleryServiceTests
    {
        private GalleryService SetupGalleryService()
        {
            var galleryRepository = new GalleryRepositoryMock();
            var pictureRepository = new PictureRepositoryMock();
            var tagRepository = new TagRepositoryMock();
            var galleryGenerator = new GalleryGeneratorFactory(tagRepository, pictureRepository, galleryRepository);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperGalleryProfile());
            });

            var galleryService = new GalleryService(galleryRepository, galleryGenerator, mapperConfig.CreateMapper());

            return galleryService;
        }

        [TestMethod]
        public async Task Test_GetCustomizedRandom_OnlyGifs()
        {
            var galleryService = SetupGalleryService();
            var galleryResponse = await galleryService.GetCustomizedRandom(1, "", "", "onlygifs");

            Assert.IsTrue(galleryResponse.GalleryPictures.Count() > 0);
        }
    }
}
