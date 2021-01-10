using System;
using DivisorPrimo.Domain.Models;

namespace DivisorPrimo.Domain.Commands
{
    public class NumeroCommand : CommandModel
    {
        public NumeroCommand(int numero)
        {
            this.NumeroBase = numero;
        }
        public int NumeroBase { get; set; }
        public string TraceId { get; set; }
    }
}