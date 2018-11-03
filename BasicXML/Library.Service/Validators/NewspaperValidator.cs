using FluentValidation;
using Library.Infrastructure.Models;
using Library.Service.Resources;

namespace Library.Service.Validators
{
    /// <summary>
    /// Validate newspaper instance.
    /// </summary>
    public class NewspaperValidator : AbstractValidator<Newspaper>
    {
        /// <summary>
        /// Initialize a <see cref="NewspaperValidator"/> class.
        /// </summary>
        public NewspaperValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(ValidateMassages.NameRequared);
            RuleFor(m => m.CountPage).NotEmpty().WithMessage(ValidateMassages.CountPageRequared);
            RuleFor(m => m.ISSN).NotEmpty().WithMessage(ValidateMassages.NewspaperISSNRequared);
            RuleFor(m => m.Number).NotEmpty().WithMessage(ValidateMassages.NewspaperNumberRequared);
        }
    }
}
