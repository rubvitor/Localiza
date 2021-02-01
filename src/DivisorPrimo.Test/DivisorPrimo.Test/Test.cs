using DivisorPrimo.Domain.Models;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using DivisorPrimo.Domain.Commands;
using DivisorPrimo.Services.Business;
using System.Linq;

namespace DivisorPrimo.Test
{
    public class Test
    {
        public Test()
        {
        }

        [Fact]
        public async Task Verifica_Numeros_Primos_Retornam_Correto()
        {
            NumeroCommand command = new NumeroCommand(20);
            NumeroCommandHandler handler = new NumeroCommandHandler(new NumeroBusiness(null, null), null);

            var retorno = await handler.Handle(command, new System.Threading.CancellationToken()) as ValidationResultModel;

            retorno.IsValid.Should().BeTrue();

            var divisorPrimo = retorno.ObjectResult as DivisorPrimoModel;
            List<int> primos = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19 };
            divisorPrimo.NumerosPrimos.Should().BeEquivalentTo(primos);
        }

        [Fact]
        public async Task Verifica_Divisores_Retornam_Correto()
        {
            NumeroCommand command = new NumeroCommand(20);
            NumeroCommandHandler handler = new NumeroCommandHandler(new NumeroBusiness(null, null), null);

            var retorno = await handler.Handle(command, new System.Threading.CancellationToken()) as ValidationResultModel;

            retorno.IsValid.Should().BeTrue();

            var divisorPrimo = retorno.ObjectResult as DivisorPrimoModel;
            List<int> divisores = new List<int> { 1, 2, 4, 5, 10, 20 };
            divisorPrimo.Divisores.Should().BeEquivalentTo(divisores);
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Deve_Retornar_Excecao_Quando_Numero_For_Menor_Igual_Zero(int numero)
        {
            NumeroCommand command = new NumeroCommand(numero);
            NumeroCommandHandler handler = new NumeroCommandHandler(new NumeroBusiness(null, null), null);

            var retorno = await handler.Handle(command, new System.Threading.CancellationToken()) as ValidationResultModel;

            retorno.IsValid.Should().BeFalse();
            retorno.Errors.Should().HaveCountGreaterThan(0);
            retorno.Errors.FirstOrDefault().ErrorMessage.Should().Be("O número base deve ser maior que zero.");
        }
    }
}
