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

        public PictureService(IPictureRepository pictureRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
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
                    folderSortOrder: pictureRequest.FolderSortOrder,
                    size: pictureRequest.Size,
                    globalSortOrder: pictureRequest.GlobalSortOrder,
                    created: pictureRequest.Created
                );

            aggregate = await _pictureRepository.Save(aggregate);
            foreach (var tag in pictureRequest.Tags)
            {
                var tagAggregate = Tag.Create(tag);
                tagAggregate.AddMediaItem(pictureRequest.Id, null);

                await _tagRepository.Save(tagAggregate);
            }

            var response = _mapper.Map<PictureResponse>(aggregate);
            response.Tags = pictureRequest.Tags;

            return response;
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

        public async Task<PictureResponse> Get(string galleryId, int index)
        {
            var aggregate = await _pictureRepository.FindByGalleryIndex(galleryId, index);

            await GetTagsFromPersistenceAndAdd(aggregate);

            return _mapper.Map<PictureResponse>(aggregate);
        }

        public async Task<PictureResponse> GetByAppPath(string appPath)
        {
            var aggregate = await _pictureRepository.FindByAppPath(appPath);

            if (aggregate is null)
                return null;

            await GetTagsFromPersistenceAndAdd(aggregate);

            return _mapper.Map<PictureResponse>(aggregate);
        }

        public async Task<IEnumerable<PictureResponse>> GetPictures(string galleryId, int offset = 0)
        {
            var pictureAggregates = await _pictureRepository.FindAll(galleryId, offset);

            var list = new List<PictureResponse>();
            foreach (var aggregate in pictureAggregates)
            {
                await GetTagsFromPersistenceAndAdd(aggregate);
                var pictureResponse = _mapper.Map<PictureResponse>(aggregate);

                list.Add(pictureResponse);
            }

            return list;
        }

        private async Task GetTagsFromPersistenceAndAdd(Picture aggregate)
        {
            var tags = await _tagRepository.FindAllTagsForPicture(aggregate.Id);
            foreach (var tag in tags)
                aggregate.AddTag(tag.TagName);
        }
    }
}
