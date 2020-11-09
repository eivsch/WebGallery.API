using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using Infrastructure.Tags.DTO;
using Infrastructure.Tags.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Tags
{
    public class TagRepositoryMock : ITagRepository
    {
        public async Task<List<Tag>> GetAllUniqueTags()
        {
            var outList = new List<Tag>();

            var data = new MockDataTags().GetAll();
            var allTags = data.GroupBy(g => g.TagName).Select(s => s.Key);
            foreach (var tag in allTags)
            {
                var aggregate = Tag.Create(tag);
                aggregate.SetItemCount(data.Count(i => i.TagName == tag));

                outList.Add(aggregate);
            }

            return outList;
        }

        public async Task<IEnumerable<Tag>> FindAllTagsForPicture(string pictureId)
        {
            var data = new MockDataTags().GetAll();
            var tags = data.Where(t => t.PictureId == pictureId);

            return BuildAggregatesFromDtoCollection(tags);
        }

        public async Task<IEnumerable<Tag>> GetRandom(IEnumerable<string> tags, int maxItemsToReturn)
        {
            var data = new MockDataTags().GetAll();

            var dtoList = new List<TagDTO>();
            foreach (var tag in tags)
            {
                dtoList.AddRange(data.Where(t => t.TagName.ToLower() == tag.ToLower()));
            }

            return BuildAggregatesFromDtoCollection(dtoList.Take(maxItemsToReturn));
        }

        private List<Tag> BuildAggregatesFromDtoCollection(IEnumerable<TagDTO> dtos)
        {
            var allTags = new List<Tag>();
            foreach (var dto in dtos)
            {
                Tag aggregate = allTags.FirstOrDefault(t => t.Name == dto.TagName);
                if (aggregate is null)
                {
                    aggregate = Tag.Create(dto.TagName);
                    allTags.Add(aggregate);
                }

                aggregate.AddMediaItem(dto.PictureId, dto.Added);
            }

            return allTags;
        }

        public async Task<Tag> Save(Tag aggregate)
        {
            return aggregate;
        }

        public async Task<Tag> Find(Tag aggregate)
        {
            var tags = new MockDataTags().GetAll().Where(t => t.TagName == aggregate.Name);

            return BuildAggregatesFromDtoCollection(tags).Single();
        }



        public Task<IEnumerable<Tag>> FindAll(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tag aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
