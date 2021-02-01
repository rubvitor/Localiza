using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DivisorPrimo.Domain.Interfaces;
using DivisorPrimo.Domain.Models;
using DivisorPrimo.Services.Redis;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace DivisorPrimo.Domain.Commands
{
    public class NumeroCommandHandler : CommandHandler,
        IRequestHandler<NumeroCommand, object>
    {
        private readonly INumeroBusiness _numeroBusiness;
        private readonly IRedisClient _redisClient;
        public NumeroCommandHandler(INumeroBusiness numeroBusiness, IRedisClient redisClient)
        {
            this._numeroBusiness = numeroBusiness;
            _redisClient = redisClient;
        }

        public async Task<object> Handle(NumeroCommand message, CancellationToken cancellationToken)
        {
            if (message.NumeroBase <= 0) return new ValidationResultModel
            {
                IsValid = false,
                Errors =
                new List<ValidationFailure> { new ValidationFailure("NumeroBase", "O número base deve ser maior que zero.") }
            };

            DivisorPrimoModel divisorPrimoModel = new DivisorPrimoModel(message.NumeroBase);
            divisorPrimoModel.TraceId = message.TraceId;

            try
            {
                if (!string.IsNullOrEmpty(message.TraceId))
                {
                    var numerosPrimosCache = await _redisClient.RetornaDivisorPrimo(message.NumeroBase);
                    if (numerosPrimosCache == null)
                        Task.Run(() => _numeroBusiness.CalculaDivisoresPrimos(message.NumeroBase, message.TraceId));
                    else
                        return numerosPrimosCache;
                }
                else
                    divisorPrimoModel = _numeroBusiness.RetornaDivisoresPrimos(message.NumeroBase);
            }

            catch (Exception ex)
            {
                AddError(ex.Message);
                var errors = new List<ValidationFailure>();
                errors.Add(new ValidationFailure("create", ex.Message));

                return new ValidationResultModel { IsValid = false, Errors = errors };
            }

            return new ValidationResultModel
            {
                ObjectResult = divisorPrimoModel
            };
        }

        public void Dispose()
        {

        }
    }
}