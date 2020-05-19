using Application.Services.Interfaces;
using Application.Tags;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using Infrastructure.Tags;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            else if (tagRequest.PictureIndex! < 1)
                pic = await _pictureRepository.FindByIndex(tagRequest.PictureIndex);
            else
                throw new ArgumentException("Must have either a picture id or index in order to add a tag");

            if (pic is null)
                throw new ArgumentException($"Cannot add new tag as a picture with id '{tagRequest.PictureId}' / index '{tagRequest.PictureIndex}' does not exist.");

            var aggregate = Map(tagRequest);

            await _tagRepository.Save(aggregate);
        }

        public async Task<IEnumerable<string>> GetAllUniqueTags()
        {
            return await _tagRepository.GetAllUniqueTags();
        }

        private Tag Map(TagRequest request)
        {
            return Tag.Create(tagName: request.Tag, pictureId: request.PictureId);
        }
    }
}
