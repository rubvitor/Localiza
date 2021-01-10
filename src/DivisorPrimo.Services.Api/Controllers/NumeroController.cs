using System;
using System.Threading.Tasks;
using DivisorPrimo.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DivisorPrimo.Services.Api.Controllers
{
    [Authorize]
    public class NumeroController : ApiController
    {
        private readonly INumeroAppService _NumeroAppService;

        public NumeroController(INumeroAppService NumeroAppService)
        {
            _NumeroAppService = NumeroAppService;
        }

        [HttpGet("numero-management/{numero:int}/{traceId}")]
        public async Task<IActionResult> Get(int numero, string traceId)
        {
            try
            {
                return CustomResponseModel(await _NumeroAppService.CalculaNumeros(numero, traceId));
            }
            catch (Exception ex)
            {
                var a = ex;
            }

            return null;
        }
    }
}
