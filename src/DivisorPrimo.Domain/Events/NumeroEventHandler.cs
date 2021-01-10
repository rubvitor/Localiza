using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DivisorPrimo.Domain.Events
{
    public class NumeroEventHandler :
        INotificationHandler<NumeroRegisteredEvent>
    {
        public Task Handle(NumeroRegisteredEvent message, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}