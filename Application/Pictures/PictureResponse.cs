using Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Pictures
{
    public class PictureResponse : IServiceResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AppPath { get; set; }
        public string OriginalPath { get; set; }
        public string FolderName { get; set; }
        public string FolderId { get; set; }
        public int GlobalSortOrder { get; set; }
        public int FolderSortOrder { get; set; }
        public int Size { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> DetectedObjects { get; set; }
    }
}
