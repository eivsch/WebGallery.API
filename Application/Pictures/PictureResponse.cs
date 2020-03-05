using Application.Common.Interfaces;

namespace Application.Pictures
{
    public class PictureResponse : IServiceResponse
    {
        public string Id { get; set; }
        public string Path { get; set; }
    }
}
