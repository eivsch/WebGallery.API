using Application.Services.Interfaces;
using Application.Tags;
using AutoMapper;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
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

        public async Task AddTag(Tag tagRequest)
        {
            var aggregate = DomainModel.Aggregates.Tag.Tag.Create(tagRequest.Name);
            foreach (var item in tagRequest.MediaItems)
            {
                Picture pic = await _pictureRepository.FindById(item.Id)
                    ?? await _pictureRepository.FindByAppPath(item.AppPath);

                if (pic is null && item.GlobalIndex.HasValue) 
                    pic = await _pictureRepository.FindByIndex(item.GlobalIndex.Value);
                    
                if (pic is null)
                    throw new ArgumentException($"Cannot add new tag as a picture with id '{item.Id}' / appPath '{item.AppPath}' / index '{item.GlobalIndex}' does not exist.");

                aggregate.AddMediaItem(pic.Id, pic.AppPath, item.Created);
            }

            await _tagRepository.Save(aggregate);
        }

        public async Task<int> DeleteTag(string pictureId, string tagName)
        {
            if (string.IsNullOrWhiteSpace(pictureId))
                throw new ArgumentNullException($"Parameter 'pictureId' is empty.");
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException($"Parameter 'tagName' is empty.");

            int deletedCount = await _tagRepository.DeleteTag(pictureId, tagName);

            return deletedCount;
        }

        public async Task<Tag> Get(string tagName)
        {
            var aggregate = DomainModel.Aggregates.Tag.Tag.Create(tagName);
            aggregate = await _tagRepository.Find(aggregate);

            var response = _mapper.Map<Tag>(aggregate);

            return response;
        }

        public async Task<IEnumerable<Tag>> GetAllUniqueTags()
        {
            var allTags = await _tagRepository.GetAllUniqueTags();

            var response = new List<Tag>();
            foreach (var tag in allTags)
            {
                var i = _mapper.Map<Tag>(tag);
                response.Add(i);
            }

            return response;
        }
    }
}
