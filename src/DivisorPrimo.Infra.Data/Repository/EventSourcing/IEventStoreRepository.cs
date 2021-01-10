using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DivisorPrimo.Domain.Core.Events;

namespace DivisorPrimo.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(string aggregateId);
    }
}