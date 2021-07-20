using FluentValidation;

namespace Carrental.Models
{
    public class SearchValidator : AbstractValidator<SearchViewModel>
    {
        public SearchValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date can not be empty");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date can not be empty");
            RuleFor(x => x.EndDate).GreaterThan(t => t.StartDate).WithMessage("Start date can not be greater than End date");
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required");
        }
    }
}
