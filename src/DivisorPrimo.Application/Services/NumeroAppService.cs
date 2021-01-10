using DivisorPrimo.Application.Interfaces;
using DivisorPrimo.Domain.Commands;
using DivisorPrimo.Domain.Models;
using MediatR;
using System.Threading.Tasks;

namespace DivisorPrimo.Application.Services
{
    public class NumeroAppService : INumeroAppService
    {
        private readonly IMediator _mediatorRoot;
        public NumeroAppService(IMediator mediatorRoot)
        {
            _mediatorRoot = mediatorRoot;
        }

        public async Task<ValidationResultModel> CalculaNumeros(int numero, string traceId)
        {
            var registerCommand = new NumeroCommand(numero);
            registerCommand.TraceId = traceId;

            return ((ValidationResultModel)await _mediatorRoot.Send<object>(registerCommand));
        }

        public void Dispose()
        {

        }
    }
}
