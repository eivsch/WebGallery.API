using API.Utilities;
using System.Text.Json.Serialization;

namespace Application.Models.Metadata
{
    [JsonConverter(typeof(AsRuntimeTypeJsonConverter<MetadataDetails>))]
    public abstract class MetadataDetails
    {
    }
}
