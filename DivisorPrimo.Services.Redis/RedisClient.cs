using DivisorPrimo.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace DivisorPrimo.Services.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IMemoryCache _memoryCache;
        public RedisClient(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<DivisorPrimoModel> RetornaDivisorPrimo(int chave)
        {
            DivisorPrimoModel divisorPrimoModel;
            if (_memoryCache.TryGetValue(chave, out divisorPrimoModel))
                return divisorPrimoModel;

            return null;
        }

        public async Task<Tuple<bool, DivisorPrimoModel>> VerificaExisteValor(int chave, int valor, bool primo = true)
        {
            var divisorPrimoModel = await RetornaDivisorPrimo(chave);
            if (divisorPrimoModel != null)
            {
                if (primo && divisorPrimoModel.NumerosPrimos != null
                    && divisorPrimoModel.NumerosPrimos.Count > 0 && divisorPrimoModel.NumerosPrimos.Any(x => x == valor))
                    return Tuple.Create(true, divisorPrimoModel);
                else if (divisorPrimoModel.NumerosPrimos != null
                    && divisorPrimoModel.Divisores.Count > 0 && divisorPrimoModel.Divisores.Any(x => x == valor))
                    return Tuple.Create(true, divisorPrimoModel);
            }

            return Tuple.Create(false, divisorPrimoModel); ;
        }

        public async Task AdicionaDivisorPrimo(int chave, int valor, bool primo = true)
        {
            var existe = await VerificaExisteValor(chave, valor, primo);
            if (!existe.Item1)
            {
                DivisorPrimoModel divisorPrimoModel = existe.Item2;
                if (existe.Item2 == null)
                    divisorPrimoModel = new DivisorPrimoModel(Convert.ToInt32(chave));

                var cacheExpirationOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.MaxValue,
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.MaxValue
                };

                _memoryCache.Set(chave, divisorPrimoModel, cacheExpirationOptions);
            }
        }
    }
}
