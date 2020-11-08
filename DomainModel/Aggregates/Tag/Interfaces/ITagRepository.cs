using DomainModel.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Tag.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<Tag>> GetAllUniqueTags();
        Task<IEnumerable<Tag>> FindAllTagsForPicture(string pictureId);
        Task<Tag> Find(string tagName);
        Task<IEnumerable<Tag>> GetRandom(IEnumerable<string> tags, int items);
    }
}
