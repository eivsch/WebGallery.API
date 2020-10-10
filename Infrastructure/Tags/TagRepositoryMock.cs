using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tags
{
    public class TagRepositoryMock : ITagRepository
    {
        public Task<Tag> Find(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> FindAll(string tagName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> FindAll(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> FindAllTagsForPicture(string pictureId)
        {
            return new List<Tag>
            {
                Tag.Create( "Tag1", pictureId),
                Tag.Create( "Tag2", pictureId),
                Tag.Create( "Tag3", pictureId),
            };
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

        public Task<List<string>> GetAllUniqueTags()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> GetRandom(IEnumerable<string> tags, int items)
        {
            return new List<Tag>
            {
                Tag.Create( "favorite", "3"),
                Tag.Create( "tag1", "7"),
                Tag.Create( "tag1", "9"),
            };
        }

        public void Remove(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> Save(Tag aggregate)
        {
            return aggregate;
        }
    }
}
