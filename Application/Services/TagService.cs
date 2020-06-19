using Application.Services.Interfaces;
using Application.Tags;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TagService : ITagService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly ITagRepository _tagRepository;

        public TagService(IPictureRepository pictureRepository, ITagRepository tagRepository)
        {
            _pictureRepository = pictureRepository;
            _tagRepository = tagRepository;
        }

        public async Task AddTag(TagRequest tagRequest)
        {
            Picture pic;
            if (!string.IsNullOrWhiteSpace(tagRequest.PictureId))
                pic = await _pictureRepository.FindById(tagRequest.PictureId);
            else if (tagRequest.PictureIndex > 0)
                pic = await _pictureRepository.FindByIndex(tagRequest.PictureIndex);
            else if (!string.IsNullOrWhiteSpace(tagRequest.AppPath))
                pic = await _pictureRepository.FindByAppPath(tagRequest.AppPath);
            else
                throw new ArgumentException("Must have either a picture id or index in order to add a tag");

            if (pic is null)
                throw new ApplicationException($"Cannot add new tag as a picture with id '{tagRequest.PictureId}' / index '{tagRequest.PictureIndex}' does not exist.");

            var aggregate = Tag.Create(tagRequest.Tag, pic.Id);

            await _tagRepository.Save(aggregate);
        }

        public async Task<IEnumerable<TagResponse>> GetAll(string tagName)
        {
            var tags = await _tagRepository.FindAll(tagName);

            return tags.Select(s => Map(s));
        }

        public async Task<IEnumerable<string>> GetAllUniqueTags()
        {
            return await _tagRepository.GetAllUniqueTags();
        }

        private TagResponse Map(Tag aggregate)
        {
            return new TagResponse 
            { 
                PictureId = aggregate.PictureId, 
                TagName = aggregate.TagName 
            };
        }
    }
}
