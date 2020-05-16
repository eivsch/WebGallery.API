using Application.Services.Interfaces;
using Application.Tags;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
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
        private readonly ITagRepositoy _tagRepository;

        public TagService(IPictureRepository pictureRepository, ITagRepositoy tagRepository)
        {
            _pictureRepository = pictureRepository;
            _tagRepository = tagRepository;
        }

        // TODO: Create a tag repository to more efficiently add tags
        public async Task AddTag(TagRequest tagRequest)
        {
            Picture aggregate;
            if (!string.IsNullOrWhiteSpace(tagRequest.PictureId))
                aggregate = await _pictureRepository.FindById(tagRequest.PictureId);
            else if (tagRequest.PictureIndex !< 1)
                aggregate = await _pictureRepository.FindByIndex(tagRequest.PictureIndex);
            else
                throw new ArgumentException("Must have a valid picture id in order to add a new tag.");

            aggregate.AddTag(tagRequest.Tag);

            await _pictureRepository.Save(aggregate);
        }

        public async Task<IEnumerable<string>> GetAllUniqueTags()
        {
            return await _tagRepository.GetAllUniqueTags();
        }
    }
}
