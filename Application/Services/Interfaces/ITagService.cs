using Application.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITagService
    {
        Task AddTag(TagRequest tagRequest);
        Task<IEnumerable<string>> GetAllUniqueTags();
        Task<IEnumerable<TagResponse>> GetAll(string tagName);
    }
}
