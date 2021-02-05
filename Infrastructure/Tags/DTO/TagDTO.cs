using System;

namespace Infrastructure.Tags.DTO
{
    internal class TagDTO
    {
        public string TagName { get; set; }
        public string PictureId { get; set; }
        public string PictureAppPath { get; set; }
        public DateTime Added { get; set; }
    }
}
