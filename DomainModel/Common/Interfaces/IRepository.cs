using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Common.Interfaces
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task<TAggregate> FindById(Guid id);

        Task<TAggregate> FindById(string id);

        Task<TAggregate> FindById(int id);

        Task<TAggregate> Find(TAggregate aggregate);

        Task<IEnumerable<TAggregate>> FindAll(TAggregate aggregate);

        Task<TAggregate> Save(TAggregate aggregate);

        void Remove(TAggregate aggregate);
    }
}
