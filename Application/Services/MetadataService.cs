using Application.Models.Metadata;
using Application.Services.Interfaces;
using AutoMapper;
using DomainModel.Aggregates.Metadata;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly Infrastructure.Services.IMetadataService _metadataService;
        private readonly IMapper _mapper;

        public MetadataService(Infrastructure.Services.IMetadataService metadataService, IMapper mapper)
        {
            _metadataService = metadataService;
            _mapper = mapper;
        }

        public async Task<MetadataResponse> Get(string itemType)
        {
            Metadata aggregate;
            MetadataType metadataType = MetadataType.Get(itemType);
            if (metadataType == MetadataType.Album)
            {
                var data = await _metadataService.GetAlbumMetadata();

                aggregate = Metadata.Create(
                    metadataType: metadataType,
                    totalCount: data.Count,
                    mostLikedName: data.MostLikedName,
                    mostRecentName: data.MostRecentName,
                    mostLikedCount: data.MostLikedCount,
                    mostRecentTs: data.MostRecentTimestamp
                );
            }
            else if (metadataType == MetadataType.Picture)
            {
                var data = await _metadataService.GetPictureMetadata();

                aggregate = Metadata.Create(
                    metadataType: metadataType,
                    totalCount: data.Count,
                    mostLikedName: data.MostLikedName,
                    mostRecentName: data.MostRecentName,
                    mostLikedCount: data.MostLikedCount,
                    mostRecentTs: data.MostRecentTimestamp
                );
            }
            else
                throw new NotImplementedException();

            return _mapper.Map<MetadataResponse>(aggregate);
        }
    }
}
