using DivisorPrimo.Domain.Models;
using System.Threading.Tasks;

namespace DivisorPrimo.Domain.Interfaces
{
    public interface INumeroBusiness
    {
        void CalculaDivisoresPrimos(int numero, string traceId);
        DivisorPrimoModel RetornaDivisoresPrimos(int numero);
    }
}
