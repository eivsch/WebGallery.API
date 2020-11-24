using DomainModel.Aggregates.Metadata.Interfaces;
using DomainModel.Common;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Metadata.Details
{
    public class MetadataTagDetails : ValueObject, IMetadataDetails
    {
        private string _mostPopularName;
        private string _mostRecentMediaName;
        private string _mostRecentTagName;
        private int? _mostPopularCount;

        public virtual string MostPopularName => _mostPopularName;
        public virtual string MostRecentMediaName => _mostRecentMediaName;
        public virtual string MostRecentTagName => _mostRecentTagName;
        public virtual int? MostPopularCount => _mostPopularCount;

        private MetadataTagDetails()
        {

        }

        public static MetadataTagDetails Create(
            string mostPopularName = "", 
            string mostRecentMediaName = "", 
            string mostRecentTagName = "", 
            int? mostPopularCount = null)
        {
            return new MetadataTagDetails
            {
                _mostPopularName = mostPopularName,
                _mostRecentMediaName = mostRecentMediaName,
                _mostRecentTagName = mostRecentTagName,
                _mostPopularCount = mostPopularCount
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
