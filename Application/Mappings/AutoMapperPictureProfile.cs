using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class AutoMapperPictureProfile : Profile
    {
        public AutoMapperPictureProfile()
        {
            CreateMap<DomainModel.Aggregates.Picture.Picture, Pictures.PictureResponse>();
        }
    }
}
