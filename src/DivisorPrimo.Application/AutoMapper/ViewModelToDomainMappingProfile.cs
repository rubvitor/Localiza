using AutoMapper;
using DivisorPrimo.Application.ViewModels;
using DivisorPrimo.Domain.Commands;
using DivisorPrimo.Domain.Models;
using FluentValidation.Results;

namespace DivisorPrimo.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<DivisorPrimoViewModel, NumeroCommand>()
                .ConstructUsing(c => new NumeroCommand(c.DivisorPrimo.NumeroBase));
            CreateMap<ValidationResultModel, ValidationResult>();
        }
    }
}
