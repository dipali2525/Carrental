using FluentValidation;

namespace Carrental.Models
{
    public class CarTypeValidator : AbstractValidator<CarTypeViewModel>
    {
        public CarTypeValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title can not be empty");
        }
    }
}
