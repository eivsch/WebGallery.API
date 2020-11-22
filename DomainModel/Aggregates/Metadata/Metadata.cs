using DomainModel.Aggregates.Metadata.Details;
using DomainModel.Aggregates.Metadata.Interfaces;
using DomainModel.Common.Interfaces;
using System;

namespace DomainModel.Aggregates.Metadata
{
    public class Metadata : IAggregateRoot
    {
        private int _totalCount;
        MetadataType _metadataType;
        private IMetadataDetails _details;

        public virtual int TotalCount => _totalCount;
        public virtual MetadataType MetadataType => _metadataType;
        public virtual IMetadataDetails Details => _details;

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
            else
                throw new NotImplementedException();

            return new Metadata
            {
                _totalCount = totalCount,
                _details = details
            };
        }
    }
}
