using FluentValidation;

namespace Carrental.Models
{
    public class OrderValidator : AbstractValidator<OrderViewModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.CarId).NotEmpty().WithMessage("Please select car");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date can not be empty");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date can not be empty");
            RuleFor(x => x.EndDate).GreaterThan(t=>t.StartDate).WithMessage("Start date can not be greater than End date");
            RuleFor(x => x.PickLocation).NotEmpty().WithMessage("Pick Location can not be empty");
            RuleFor(x => x.DropLocation).NotEmpty().WithMessage("Drop Location can not be empty");
            RuleFor(x => x.ContactNo).NotEmpty().WithMessage("Contact No can not be empty");
            RuleFor(x => x.ContactPerson).NotEmpty().WithMessage("Contact Person can not be empty");
        }
    }
}
