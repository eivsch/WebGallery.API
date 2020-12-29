using System;
using System.Collections.Generic;

namespace Application.Pictures
{
    public class PictureRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AppPath { get; set; }
        public string OriginalPath { get; set; }
        public string FolderName { get; set; }
        public string FolderAppPath { get; set; }
        public int? FolderSortOrder { get; set; }
        public int? GlobalSortOrder { get; set; }
        public int Size { get; set; }
        public IEnumerable<string> Tags { get; set; } = new List<string>();
        public DateTime CreateTimestamp { get; set; }
    }
}
