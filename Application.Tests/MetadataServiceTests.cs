using Application.Models.Metadata;
using Application.Services;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    [TestClass]
    public class MetadataServiceTests
    {
        private MetadataService SetupMetadataService()
        {
            var metadataInfraService = new Infrastructure.Services.MetadataServiceMock();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperMetadataProfile());
            });

            var metadataService = new MetadataService(metadataInfraService, mapperConfig.CreateMapper());

            return metadataService;
        }

        [TestMethod]
        public async Task Test_GetPictureMetadata()
        {
            var metaService = SetupMetadataService();
            var metaResponse = await metaService.Get("picture");
            var details = (MetadataPictureDetails) metaResponse.Details;

            Assert.IsFalse(string.IsNullOrWhiteSpace(metaResponse.Name));
            Assert.IsTrue(metaResponse.TotalCount > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostLikedName));
            Assert.IsNotNull(details.MostLikedCount);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostRecentName));
            Assert.IsNotNull(details.MostRecentTimestamp);
        }

        [TestMethod]
        public async Task Test_GetGifMetadata()
        {
            var metaService = SetupMetadataService();
            var metaResponse = await metaService.Get("gif");
            var details = (MetadataGifDetails) metaResponse.Details;

            Assert.IsFalse(string.IsNullOrWhiteSpace(metaResponse.Name));
            Assert.IsTrue(metaResponse.TotalCount > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostLikedName));
            Assert.IsNotNull(details.MostLikedCount);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostRecentName));
            Assert.IsNotNull(details.MostRecentTimestamp);
        }

        [TestMethod]
        public async Task Test_GetVideoMetadata()
        {
            var metaService = SetupMetadataService();
            var metaResponse = await metaService.Get("video");
            var details = (MetadataVideoDetails) metaResponse.Details;

            Assert.IsFalse(string.IsNullOrWhiteSpace(metaResponse.Name));
            Assert.IsTrue(metaResponse.TotalCount > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostLikedName));
            Assert.IsNotNull(details.MostLikedCount);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostRecentName));
            Assert.IsNotNull(details.MostRecentTimestamp);
        }

        [TestMethod]
        public async Task Test_GetAlbumMetadata()
        {
            var metaService = SetupMetadataService();
            var metaResponse = await metaService.Get("album");
            var details = (MetadataAlbumDetails) metaResponse.Details;

            Assert.IsFalse(string.IsNullOrWhiteSpace(metaResponse.Name));
            Assert.IsTrue(metaResponse.TotalCount > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostLikedName));
            Assert.IsNotNull(details.MostLikedCount);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostRecentName));
            Assert.IsNotNull(details.MostRecentTimestamp);
        }

        [TestMethod]
        public async Task Test_GetTagMetadata()
        {
            var metaService = SetupMetadataService();
            var metaResponse = await metaService.Get("tag");
            var details = (MetadataTagDetails) metaResponse.Details;

            Assert.IsFalse(string.IsNullOrWhiteSpace(metaResponse.Name));
            Assert.IsTrue(metaResponse.TotalCount > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostPopularName));
            Assert.IsNotNull(details.MostPopularCount);
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostRecentMediaName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(details.MostRecentTagName));
        }
    }
}
