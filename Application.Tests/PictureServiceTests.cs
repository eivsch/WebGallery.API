using Application.Pictures;
using Application.Services;
using AutoMapper;
using Infrastructure.Pictures;
using Infrastructure.Tags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
                GlobalSortOrder = 33324,
                Tags = new List<string> { "tag1, tag2" },
                Created = DateTime.Now
            };

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock(), mapperConfig.CreateMapper());
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
                GlobalSortOrder = 33324,
                Tags = new List<string> { "tag1, tag2" },
                Created = DateTime.Now
            };

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock(), mapperConfig.CreateMapper());
            var response = await service.Add(request);

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Id));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.AppPath));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.OriginalPath));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.FolderName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.FolderId));
            Assert.AreEqual(365, response.FolderSortOrder);
            Assert.AreEqual(39943, response.Size);
            Assert.AreEqual(33324, response.GlobalSortOrder);
            Assert.IsNotNull(response.Tags);
            Assert.IsNotNull(response.CreateTimestamp);
        }
    }
}
