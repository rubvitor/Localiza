using DivisorPrimo.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DivisorPrimo.Domain.Models;

namespace DivisorPrimo.Services.Business
{
    public class NumeroBusiness : Hub, INumeroBusiness
    {
        string nomeFilaDivisor = "divisor";
        string nomeFilaPrimo = "primo";
        protected IHubContext<NumeroBusiness> _context;
        public NumeroBusiness(IHubContext<NumeroBusiness> context)
        {
            _context = context;
        }

        public void CalculaDivisoresPrimos(int numero, string traceId)
        {
            Task.Run(() => CalculaPrimos(numero, traceId));
            Task.Run(() => CalcularDivisores(numero, traceId));
        }

        private async Task CalcularDivisores(int numero, string traceId)
        {
            if (numero == 1)
            {
                _context.Clients.All.SendAsync(nomeFilaDivisor, traceId, numero);
                return;
            }

            _context.Clients.All.SendAsync(nomeFilaDivisor, traceId, 1);

            Parallel.For(2, (numero / 2) + 1, idx =>
            {
                if (numero % idx == 0)
                    _context.Clients.All.SendAsync(nomeFilaDivisor, traceId, idx);
            });

            _context.Clients.All.SendAsync(nomeFilaDivisor, traceId, numero);
        }

        private List<int> RetornaDivisores(int numero)
        {
            List<int> divisores = new List<int>();
            divisores.Add(1);

            if (numero == 1)
                return divisores;

            var query =
            from idx in ParallelEnumerable.Range(2, (numero / 2) + 1)
            where numero % idx == 0
            select idx;

            query.ForAll((num) => divisores.Add(num));
            divisores.Add(numero);

            return divisores?.Distinct()?.OrderBy(x => x)?.ToList();
        }

        private async Task CalculaPrimos(int numero, string traceId)
        {
            //No exemplo do .doc enviado considera o número 1 como primo, porém número 1 não é primo.
            //https://pt.wikipedia.org/wiki/N%C3%BAmero_primo
            if (numero < 2)
                return;

            var thread = Parallel.For(2, numero, idx =>
            {
                if (VerificaPrimo(idx))
                    _context.Clients.All.SendAsync(nomeFilaPrimo, traceId, idx);
            });

            if (VerificaPrimo(numero))
                _context.Clients.All.SendAsync(nomeFilaPrimo, traceId, numero);
        }

        private bool VerificaPrimo(int numero)
        {
            var divisores = RetornaDivisores(numero);
            return divisores.Count == 2 && divisores.Any(x => x == 1) && divisores.Any(x => x == numero);
        }

        public DivisorPrimoModel RetornaDivisoresPrimos(int numero)
        {
            return new DivisorPrimoModel(numero)
            {
                Divisores = RetornaDivisores(numero),
                NumerosPrimos = RetornaPrimos(numero)
            };
        }

        private List<int> RetornaPrimos(int numero)
        {
            //No exemplo do .doc enviado considera o número 1 como primo, porém número 1 não é primo.
            //https://pt.wikipedia.org/wiki/N%C3%BAmero_primo
            if (numero < 2)
                return new List<int>();

            List<int> primos = new List<int>();

            var thread = Parallel.For(2, numero, idx =>
            {
                if (VerificaPrimo(idx))
                    primos.Add(idx);
            });

            while (!thread.IsCompleted) Thread.Sleep(1);

            if (VerificaPrimo(numero))
                primos.Add(numero);

            return primos?.OrderBy(x => x)?.ToList();
        }
    }
}
