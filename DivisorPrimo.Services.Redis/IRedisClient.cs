using DivisorPrimo.Domain.Models;
using System;
using System.Threading.Tasks;

namespace DivisorPrimo.Services.Redis
{
    public interface IRedisClient
    {
        Task AdicionaDivisorPrimo(int chave, int valor, bool primo = true);
        Task<DivisorPrimoModel> RetornaDivisorPrimo(int chave);
        Task<Tuple<bool, DivisorPrimoModel>> VerificaExisteValor(int chave, int valor, bool primo = true);
    }
}