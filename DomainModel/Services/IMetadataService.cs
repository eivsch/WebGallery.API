using DomainModel.Aggregates.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Services
{
    public interface IMetadataService
    {
        public Metadata GetPictureMetadata();
        public Metadata GetGifMetadata();
        public Metadata GetTagMetadata();
        public Metadata GetVideoMetadata();
        public Metadata GetAlbumMetadata();
    }
}
