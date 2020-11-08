using Application.Tags;
using AutoMapper;

namespace Application.Mappings
{
    public class AutoMapperTagProfile : Profile
    {
        public AutoMapperTagProfile()
        {
            CreateMap<DomainModel.Aggregates.Tag.Tag, TagResponse>();
            CreateMap<DomainModel.Aggregates.Tag.TagMediaItem, TagMediaItem>()
                .ForMember(dest => dest.TagItemId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
