using Application.Pictures;
using Application.Services.Interfaces;
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

        public PictureService(IPictureRepository pictureRepository, ITagRepository tagRepository)
        {
            _pictureRepository = pictureRepository;
            _tagRepository = tagRepository;
        }

        public async Task<PictureResponse> Add(PictureRequest pictureRequest)
        {
            Picture pic = Picture.Create
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

            foreach (var tag in pictureRequest.Tags)
            {
                var tagAggregate = Tag.Create(tag, pic.Id);
                await _tagRepository.Save(tagAggregate);
            }

            pic = await _pictureRepository.Save(pic);

            return Map(pic);
        }

        public async Task<PictureResponse> Get(string pictureId)
        {
            var pic = await _pictureRepository.FindById(pictureId);

            await GetTagsFromPersistenceAndAdd(pic);

            return Map(pic);
        }

        public async Task<PictureResponse> Get(int index)
        {
            var pic = await _pictureRepository.FindByIndex(index);

            await GetTagsFromPersistenceAndAdd(pic);

            return Map(pic);
        }

        public async Task<PictureResponse> Get(string galleryId, int pictureId)
        {
            var pic = await _pictureRepository.FindByGalleryIndex(galleryId, pictureId);

            await GetTagsFromPersistenceAndAdd(pic);

            return Map(pic);
        }

        public async Task<IEnumerable<PictureResponse>> GetPictures(string galleryId, int offset = 0)
        {
            var pics = await _pictureRepository.FindAll(galleryId, offset);

            // TODO: Add tags

            var list = new List<PictureResponse>();
            pics.ToList().ForEach(p => list.Add(Map(p)));

            return list;
        }

        private async Task GetTagsFromPersistenceAndAdd(Picture aggregate)
        {
            var tags = await _tagRepository.FindAllTagsForPicture(aggregate.Id);
            foreach (var tag in tags)
                aggregate.AddTag(tag.TagName);
        }

        private PictureResponse Map(Picture pic)
        {
            return new PictureResponse
            {
                Id = pic.Id,
                Name = pic.Name,
                AppPath = pic.AppPath,
                OriginalPath = pic.OriginalPath,
                FolderName = pic.FolderName,
                FolderId = pic.FolderId,
                FolderSortOrder = pic.FolderSortOrder,
                GlobalSortOrder = pic.GlobalSortOrder,
                Size = pic.Size,
                CreateTimestamp = pic.CreateTimestamp,
                Tags = pic.Tags
            };
        }
    }
}
