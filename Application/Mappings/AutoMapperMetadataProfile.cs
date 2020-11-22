using Application.Models.Metadata;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Application.Mappings
{
    public class AutoMapperMetadataProfile : Profile
    {
        public AutoMapperMetadataProfile()
        {
            CreateMap<DomainModel.Aggregates.Metadata.Metadata, MetadataResponse>()
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.MetadataType.Name));

            CreateMap<DomainModel.Aggregates.Metadata.Details.MetadataPictureDetails, MetadataPictureDetails>();
        }
    }
}
