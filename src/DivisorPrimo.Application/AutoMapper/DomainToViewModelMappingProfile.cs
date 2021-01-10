using AutoMapper;
using DivisorPrimo.Application.ViewModels;
using DivisorPrimo.Domain.Models;
using FluentValidation.Results;

namespace DivisorPrimo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<DivisorPrimoModel, DivisorPrimoViewModel>();
            CreateMap<ValidationResult, ValidationResultModel>();
        }
    }
}
