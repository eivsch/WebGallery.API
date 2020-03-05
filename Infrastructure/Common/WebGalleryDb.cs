using DomainModel.Exceptions;

namespace Infrastructure.Common
{
    public sealed class WebGalleryDb : BaseDb, IWebGalleryDb
    {
        public WebGalleryDb(string connectionString, int defaultTimeout = 15) : base(connectionString, defaultTimeout)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InfrastructureLayerException($"{nameof(connectionString)} is empty");
        }
    }
}
