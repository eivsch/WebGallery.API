using Application.Services.Interfaces;
using Application.Tags;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public TagService(IPictureRepository pictureRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
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

            var aggregate = Tag.Create(tagRequest.Tag);
            aggregate.AddMediaItem(pic.Id, null);

            await _tagRepository.Save(aggregate);
        }

        public async Task<TagResponse> Get(string tagName)
        {
            var aggregate = Tag.Create(tagName);
            aggregate = await _tagRepository.Find(aggregate);

            var response = _mapper.Map<TagResponse>(aggregate);

            return response;
        }

        public async Task<IEnumerable<TagResponse>> GetAllUniqueTags()
        {
            var allTags = await _tagRepository.GetAllUniqueTags();

            var response = new List<TagResponse>();
            foreach (var tag in allTags)
            {
                var i = _mapper.Map<TagResponse>(tag);
                response.Add(i);
            }

            return response;
        }
    }
}
