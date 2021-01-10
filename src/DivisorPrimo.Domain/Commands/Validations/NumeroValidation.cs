using FluentValidation;

namespace DivisorPrimo.Domain.Commands.Validations
{
    public abstract class NumeroValidation<T> : AbstractValidator<T> where T : NumeroCommand
    {
        protected void ValidateDivisorPrimo()
        {
            RuleFor(c => c.NumeroBase)
                .NotEmpty().WithMessage("Please send numero for DivisorPrimo");
        }
    }
}