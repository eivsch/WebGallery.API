using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Tag.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<List<string>> GetAllUniqueTags();
        Task<IEnumerable<Tag>> FindAllTagsForPicture(string pictureId);
        Task<IEnumerable<Tag>> FindAll(string tagName);
    }
}
