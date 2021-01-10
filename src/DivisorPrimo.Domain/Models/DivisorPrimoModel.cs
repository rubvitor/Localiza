using System.Collections.Generic;

namespace DivisorPrimo.Domain.Models
{
    public class DivisorPrimoModel
    {
        public DivisorPrimoModel(int numeroBase)
        {
            this.NumeroBase = numeroBase;
            if (numeroBase <= 0)
                throw new System.Exception("O número base não pode ser menor ou igual a zero.");
        }
        public int NumeroBase { get; set; }
        public List<int> NumerosPrimos { get; set; }
        public List<int> Divisores { get; set; }
        public string TraceId { get; set; }
    }
}
