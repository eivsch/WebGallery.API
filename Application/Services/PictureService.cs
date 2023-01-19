using Application.Pictures;
using Application.Services.Interfaces;
using AutoMapper;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly Infrastructure.Services.IMetadataService _metadataService;

        public PictureService(
            IPictureRepository pictureRepository, 
            ITagRepository tagRepository, 
            IMapper mapper, 
            Infrastructure.Services.IMetadataService metadataService)
        {
            _pictureRepository = pictureRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
            _metadataService = metadataService;
        }

        public async Task<PictureResponse> Add(PictureRequest pictureRequest)
        {
            Picture aggregate = Picture.Create
                (
                    appPath: pictureRequest.AppPath,
                    originalPath: pictureRequest.OriginalPath,
                    name: pictureRequest.Name,
                    folderName: pictureRequest.FolderName,
                    folderAppPath: pictureRequest.FolderAppPath,
                    folderSortOrder: pictureRequest.FolderSortOrder ?? -1,
                    size: pictureRequest.Size,
                    globalSortOrder: pictureRequest.GlobalSortOrder ?? -1,
                    created: pictureRequest.CreateTimestamp
                );

            foreach (string s in pictureRequest.DetectedObjects)
                aggregate.AddDetectedObject(s);
            
            aggregate = await _pictureRepository.Save(aggregate);
            
            foreach (var tag in pictureRequest.Tags)
            {
                var tagAggregate = Tag.Create(tag);
                tagAggregate.AddMediaItem(pictureRequest.Id, pictureRequest.AppPath, null);

                await _tagRepository.Save(tagAggregate);
            }

            var response = _mapper.Map<PictureResponse>(aggregate);
            response.Tags = pictureRequest.Tags;

            return response;
        }

        public async Task<bool> DeletePicture(string pictureId)
        {
            var aggregate = await _pictureRepository.FindById(pictureId);
            if (aggregate == null)
                return false;

            foreach (var tag in aggregate.Tags)
                await _tagRepository.DeleteTag(aggregate.Id, tag);

            await _pictureRepository.Remove(aggregate);

            return true;
        }

        public async Task<PictureResponse> Get(string pictureId)
        {
            var aggregate = await _pictureRepository.FindById(pictureId);

            await GetTagsFromPersistenceAndAdd(aggregate);

            return _mapper.Map<PictureResponse>(aggregate);
        }

        public async Task<PictureResponse> Get(int index)
        {
            var aggregate = await _pictureRepository.FindByIndex(index);

            await GetTagsFromPersistenceAndAdd(aggregate);

            return _mapper.Map<PictureResponse>(aggregate);
        }

        public async Task<PictureResponse> GetRandomFromAlbum(string albumId)
        {
            var aggregate = await _pictureRepository.FindRandomFromAlbum(albumId);

            if (aggregate is null)
                return null;

            await GetTagsFromPersistenceAndAdd(aggregate);

            return _mapper.Map<PictureResponse>(aggregate);
        }

        public async Task<IEnumerable<PictureResponse>> Search(string query)
        {
            var result = await _pictureRepository.Search(query);

            return _mapper.Map<IEnumerable<PictureResponse>>(result);
        }

        private async Task GetTagsFromPersistenceAndAdd(Picture aggregate)
        {
            var tags = await _tagRepository.FindAllTagsForPicture(aggregate.Id);
            foreach (var tag in tags)
                aggregate.AddTag(tag.Name);
        }
    }
}
