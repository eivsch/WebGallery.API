using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Tags
{
    public interface ITagRepositoy
    {
        Task<List<string>> GetAllUniqueTags();
    }
}
