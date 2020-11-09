using Infrastructure.Tags.DTO;
using System;
using System.Collections.Generic;

namespace Infrastructure.Tags.Mock
{
    internal class MockDataTags
    {
        public List<TagDTO> GetAll() => new List<TagDTO>
        {
            // Pic 1
            new TagDTO
            {
                TagName = "Tag1",
                PictureId = "1",
                Added = DateTime.Now
            },
            new TagDTO
            {
                TagName = "Tag2",
                PictureId = "1",
                Added = DateTime.Now
            },
            new TagDTO
            {
                TagName = "Car",
                PictureId = "1",
                Added = DateTime.Now
            },
            // Pic 2
            new TagDTO
            {
                TagName = "Tag2",
                PictureId = "2",
                Added = DateTime.Now
            },
            // Pic 3
            new TagDTO
            {
                TagName = "Tag3",
                PictureId = "3",
                Added = DateTime.Now
            },
            // Pic 4
            new TagDTO
            {
                TagName = "Car",
                PictureId = "4",
                Added = DateTime.Now
            },
            // Pic 5
            new TagDTO
            {
                TagName = "Bike",
                PictureId = "5",
                Added = DateTime.Now
            },
            // Pic 6
            new TagDTO
            {
                TagName = "Bike",
                PictureId = "6",
                Added = DateTime.Now
            },
        };
    }
}
