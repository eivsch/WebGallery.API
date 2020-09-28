using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Mappings
{
    public class AutoMapperGalleryProfile : Profile
    {
        public AutoMapperGalleryProfile()
        {
            CreateMap<DomainModel.Aggregates.Gallery.Gallery, Galleries.GalleryResponse>()
                .ForMember(dest => dest.ImageCount, opt => opt.MapFrom(src => src.NumberOfItems))
                .ForMember(dest => dest.GalleryPictures, opt => opt.MapFrom(serc => serc.GalleryItems));

            CreateMap<DomainModel.Aggregates.Gallery.GalleryItem, Galleries.GalleryPicture>();
        }
    }
}
