using DomainModel.Aggregates.Metadata.Interfaces;
using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Aggregates.Metadata
{
    public class Metadata : IAggregateRoot
    {
        private int _totalCount;
        private IMetadataDetails _metadataDetails;

        public virtual int TotalCount => _totalCount;
        public virtual IMetadataDetails Details => _metadataDetails;


        private Metadata()
        {

        }

        public static Metadata Create(int totalCount)
        {
            return new Metadata
            {
                _totalCount = totalCount
            };
        }

        public void AddMetric(string key, string value)
        {

        }
    }
}
