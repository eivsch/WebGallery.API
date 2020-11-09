using Application.Services;
using Application.Tags;
using AutoMapper;
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
    public class TagServiceTests
    {
        [TestMethod]
        public async Task Test_AddTag()
        {
            var tagRepositoryMock = new TagRepositoryMock();
            var pictureRepositoryMock = new PictureRepositoryMock();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperTagProfile());
            });

            var tagService = new TagService(pictureRepositoryMock, tagRepositoryMock, mapperConfig.CreateMapper());
            var request = new Tag
            {
                Name = "Tag1",
                MediaItems = new List<TagMediaItem>
                {
                    new TagMediaItem
                    {
                        Id = "1",
                    }
                }
            };

            await tagService.AddTag(request);
        }

        [TestMethod]
        public async Task Test_GetAllUniqueTags()
        {
            var tagRepositoryMock = new TagRepositoryMock();
            var pictureRepositoryMock = new PictureRepositoryMock();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperTagProfile());
            });

            var tagService = new TagService(pictureRepositoryMock, tagRepositoryMock, mapperConfig.CreateMapper());
            var result = await tagService.GetAllUniqueTags();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
            Assert.IsNotNull(result.First().Name);
            Assert.IsNotNull(result.First().ItemCount);
        }

        [TestMethod]
        public async Task Test_Get()
        {
            var tagRepositoryMock = new TagRepositoryMock();
            var pictureRepositoryMock = new PictureRepositoryMock();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings.AutoMapperTagProfile());
            });

            var tagService = new TagService(pictureRepositoryMock, tagRepositoryMock, mapperConfig.CreateMapper());
            var result = await tagService.Get("Tag1");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.MediaItems.Count() > 0);
        }
    }
}
