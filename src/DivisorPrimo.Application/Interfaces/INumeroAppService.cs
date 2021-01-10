using DivisorPrimo.Domain.Models;
using System;
using System.Threading.Tasks;

namespace DivisorPrimo.Application.Interfaces
{
    public interface INumeroAppService : IDisposable
    {
        Task<ValidationResultModel> CalculaNumeros(int numero, string traceId);
    }
}
