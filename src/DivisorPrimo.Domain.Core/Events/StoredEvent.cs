using System;
using NetDevPack.Messaging;

namespace DivisorPrimo.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public string AggregateId { get; set; }
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = string.Empty;
            AggregateId = theEvent.AggregateId.ToString();
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        // EF Constructor
        protected StoredEvent() { }

        public string Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}