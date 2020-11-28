using DomainModel.Aggregates.Metadata.Details;
using DomainModel.Aggregates.Metadata.Interfaces;
using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Metadata
{
    public class Metadata : IAggregateRoot
    {
        private int _totalCount;
        MetadataType _metadataType;
        private IMetadataDetails _details;
        private IDictionary<string, string> _metrics;

        public virtual int TotalCount => _totalCount;
        public virtual MetadataType MetadataType => _metadataType;
        public virtual IMetadataDetails Details => _details;
        // TODO: Readonly
        public virtual IDictionary<string, string> Metrics => _metrics;

        private Metadata()
        {

        }

        public static Metadata Create(
            MetadataType metadataType,
            int totalCount, 
            string mostLikedName = "", 
            string mostRecentName = "", 
            int? mostLikedCount = null, 
            DateTime? mostRecentTs = null)
        {
            IMetadataDetails details;
            if (metadataType == MetadataType.Picture)
                details = MetadataPictureDetails.Create(mostLikedName, mostRecentName, mostLikedCount, mostRecentTs);
            else if (metadataType == MetadataType.Gif)
                details = MetadataGifDetails.Create(mostLikedName, mostRecentName, mostLikedCount, mostRecentTs);
            else if (metadataType == MetadataType.Video)
                details = MetadataVideoDetails.Create(mostLikedName, mostRecentName, mostLikedCount, mostRecentTs);
            else if (metadataType == MetadataType.Album)
                details = MetadataAlbumDetails.Create(mostLikedName, mostRecentName, mostLikedCount, mostRecentTs);
            else
                throw new NotSupportedException($"The Metadata type '{nameof(metadataType.Name)}' is not supported. Use another Create() method.");

            var metrics = new Dictionary<string, string>();
            metrics.Add(nameof(totalCount), totalCount.ToString());
            metrics.Add(nameof(mostLikedName), mostLikedName);
            metrics.Add(nameof(mostRecentName), mostRecentName);
            metrics.Add(nameof(mostLikedCount), mostLikedCount?.ToString());
            metrics.Add(nameof(mostRecentTs), mostRecentTs?.ToString());

            return new Metadata
            {
                _metadataType = metadataType,
                _totalCount = totalCount,
                _details = details,
                _metrics = metrics
            };
        }

        public static Metadata Create(
            MetadataType metadataType,
            int totalCount,
            int totalUnique,
            string mostPopularName = "",
            string mostRecentMediaName = "",
            string mostRecentTagName = "",
            int? mostPopularCount = null)
        {
            IMetadataDetails details;
            if (metadataType == MetadataType.Tag)
                details = MetadataTagDetails.Create(totalUnique, mostPopularName, mostRecentMediaName, mostRecentTagName, mostPopularCount);
            else
                throw new NotSupportedException($"The Metadata type '{nameof(metadataType.Name)}' is not supported. Use another Create() method.");

            var metrics = new Dictionary<string, string>();
            metrics.Add(nameof(totalCount), totalCount.ToString());
            metrics.Add(nameof(totalUnique), totalUnique.ToString());
            metrics.Add(nameof(mostPopularName), mostPopularName);
            metrics.Add(nameof(mostRecentMediaName), mostRecentMediaName);
            metrics.Add(nameof(mostRecentTagName), mostRecentTagName);
            metrics.Add(nameof(mostPopularCount), mostPopularCount?.ToString());

            return new Metadata
            {
                _metadataType = metadataType,
                _totalCount = totalCount,
                _details = details,
                _metrics = metrics
            };
        }
    }
}
