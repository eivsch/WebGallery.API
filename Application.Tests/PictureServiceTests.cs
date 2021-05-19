using Application.Pictures;
using Application.Services;
using AutoMapper;
using Infrastructure.Pictures;
using Infrastructure.Services;
using Infrastructure.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests
{
    [TestClass]
    public class PictureServiceTests
    {
        [TestMethod]
        public async Task Test_Add_ResponseNotNull()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperPictureProfile());
            });

            var request = new PictureRequest
            {
                Id = "dsadas",
                Name = "name",
                AppPath = "app\\path",
                OriginalPath = "orig\\path",
                FolderName = "folderName",
                FolderAppPath = "folderAppPath",
                FolderSortOrder = 365,
                Size = 39943,
                Tags = new List<string> { "tag1, tag2" },
                CreateTimestamp = DateTime.Now
            };

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock(), mapperConfig.CreateMapper(), new MetadataServiceMock());
            var response = await service.Add(request);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Test_Add_AllPropertiesHaveValues()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperPictureProfile());
            });

            var request = new PictureRequest
            {
                Id = "dsadas",
                Name = "name",
                AppPath = "app\\path",
                OriginalPath = "orig\\path",
                FolderName = "folderName",
                FolderAppPath = "folderAppPath",
                FolderSortOrder = 365,
                Size = 39943,
                Tags = new List<string> { "tag1, tag2" },
                CreateTimestamp = DateTime.Now
            };

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock(), mapperConfig.CreateMapper(), new MetadataServiceMock());
            var response = await service.Add(request);

            AssertAllPropertiesHaveValues(response);
        }

        [TestMethod]
        public async Task Test_GetById()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperPictureProfile());
            });

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock(), mapperConfig.CreateMapper(), new MetadataServiceMock());
            var response = await service.Get("1");

            AssertAllPropertiesHaveValues(response);
        }

        [TestMethod]
        public async Task Test_GetByIndex()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperPictureProfile());
            });

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock(), mapperConfig.CreateMapper(), new MetadataServiceMock());
            var response = await service.Get(1);

            AssertAllPropertiesHaveValues(response);
        }

        private void AssertAllPropertiesHaveValues(PictureResponse response)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Id));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.AppPath));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.OriginalPath));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.FolderName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.FolderId));
            Assert.IsTrue(response.FolderSortOrder > 0);
            Assert.IsTrue(response.Size > 0);
            Assert.IsNotNull(response.Tags);
            Assert.IsTrue(response.Tags.Count() > 0);
            Assert.IsNotNull(response.CreateTimestamp);
        }
    }
}
