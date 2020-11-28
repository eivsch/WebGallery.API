using DomainModel.Aggregates.Metadata.Interfaces;
using DomainModel.Common;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Metadata.Details
{
    public class MetadataGifDetails : ValueObject, IMetadataDetails
    {
        private string _mostLikedName;
        private string _mostRecentName;
        private int? _mostLikedCount;
        private DateTime? _mostRecentTimestamp;

        public virtual string MostLikedName => _mostLikedName;
        public virtual string MostRecentName => _mostRecentName;
        public virtual int? MostLikedCount => _mostLikedCount;
        public virtual DateTime? MostRecentTimestamp => _mostRecentTimestamp;

        private MetadataGifDetails()
        {

        }

        public static MetadataGifDetails Create(string mostLikedName = "", string mostRecentName = "", int? mostLikedCount = null, DateTime? mostRecentTs = null)
        {
            return new MetadataGifDetails
            {
                _mostRecentTimestamp = mostRecentTs,
                _mostLikedCount = mostLikedCount,
                _mostLikedName = mostLikedName,
                _mostRecentName = mostRecentName
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
