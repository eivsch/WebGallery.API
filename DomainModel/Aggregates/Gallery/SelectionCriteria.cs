using DomainModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Aggregates.Gallery
{
    class SelectionCriteria : Enumeration
    {
        // E.g.:
        // Default (ex: TOP 1000)
        // Popularity (views)
        // Rating
        // CreatedDate
        // Color...
    }
}
