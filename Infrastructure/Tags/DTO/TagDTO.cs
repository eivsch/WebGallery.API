using System;

namespace Infrastructure.Tags.DTO
{
    internal class TagDTO
    {
        // This is the internal elasticsearch id (_id)
        public string Id { get; set; }
        public string TagName { get; set; }
        public string PictureId { get; set; }
        public string PictureAppPath { get; set; }
        public DateTime Added { get; set; }
    }
}
