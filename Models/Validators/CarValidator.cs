using FluentValidation;

namespace Carrental.Models
{
    public class CarValidator : AbstractValidator<CarViewModel>
    {
        public CarValidator()
        {
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("Type can not be empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price can not be empty");
        }
    }
}
