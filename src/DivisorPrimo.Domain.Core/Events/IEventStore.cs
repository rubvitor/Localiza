using NetDevPack.Messaging;

namespace DivisorPrimo.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}