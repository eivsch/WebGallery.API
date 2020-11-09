using Application.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITagService
    {
        Task AddTag(Tag tagRequest);
        Task<IEnumerable<Tag>> GetAllUniqueTags();
        Task<Tag> Get(string tagName);
    }
}
