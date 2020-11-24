using Application.Models.Metadata;
using AutoMapper;

namespace Application.Mappings
{
    public class AutoMapperMetadataProfile : Profile
    {
        public AutoMapperMetadataProfile()
        {
            CreateMap<DomainModel.Aggregates.Metadata.Metadata, MetadataResponse>()
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.MetadataType.Name));

            CreateMap<DomainModel.Aggregates.Metadata.Interfaces.IMetadataDetails, IMetadataDetails>()
                .Include<DomainModel.Aggregates.Metadata.Details.MetadataPictureDetails, MetadataPictureDetails>()
                .Include<DomainModel.Aggregates.Metadata.Details.MetadataGifDetails, MetadataGifDetails>()
                .Include<DomainModel.Aggregates.Metadata.Details.MetadataVideoDetails, MetadataVideoDetails>()
                .Include<DomainModel.Aggregates.Metadata.Details.MetadataAlbumDetails, MetadataAlbumDetails>()
                .Include<DomainModel.Aggregates.Metadata.Details.MetadataTagDetails, MetadataTagDetails>();
            
            CreateMap<DomainModel.Aggregates.Metadata.Details.MetadataPictureDetails, MetadataPictureDetails>();
            CreateMap<DomainModel.Aggregates.Metadata.Details.MetadataGifDetails, MetadataGifDetails>();
            CreateMap<DomainModel.Aggregates.Metadata.Details.MetadataVideoDetails, MetadataVideoDetails>();
            CreateMap<DomainModel.Aggregates.Metadata.Details.MetadataAlbumDetails, MetadataAlbumDetails>();
            CreateMap<DomainModel.Aggregates.Metadata.Details.MetadataTagDetails, MetadataTagDetails>();
        }
    }
}
