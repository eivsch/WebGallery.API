using Application.Pictures;
using Application.Services;
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
        public async Task Test_SavePicture()
        {
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

            var service = new PictureService(new PictureRepositoryMock(), new TagRepositoryMock());
            var response = await service.Add(request);

            Assert.IsNotNull(response);
        }
    }
}
