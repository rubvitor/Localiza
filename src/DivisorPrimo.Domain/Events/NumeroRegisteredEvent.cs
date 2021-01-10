using System;
using NetDevPack.Messaging;

namespace DivisorPrimo.Domain.Events
{
    public class NumeroRegisteredEvent : Event
    {
        public NumeroRegisteredEvent(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}