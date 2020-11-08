using Application.Tags;
using AutoMapper;

namespace Application.Mappings
{
    public class AutoMapperTagProfile : Profile
    {
        public AutoMapperTagProfile()
        {
            CreateMap<DomainModel.Aggregates.Tag.Tag, TagResponse>();
            CreateMap<DomainModel.Aggregates.Tag.TaggedMediaItem, TaggedItem>()
                .ForMember(dest => dest.TaggedItemId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
