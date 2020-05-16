using System.Collections.Generic;

namespace Infrastructure.Tags.DTO
{
    internal class PictureDTO
    {
        public IEnumerable<TagDTO> Tags { get; set; }
    }

    internal class TagDTO
    {
        public string Name { get; set; }
    }
}
