using Application.Metadata;

namespace Application.Services.Interfaces
{
    public interface IMetadataService
    {
        MetadataResponse Get(string itemType);
    }
}
