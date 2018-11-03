using FluentValidation;
using Library.Infrastructure.Models;
using Library.Service.Resources;

namespace Library.Service.Validators
{
    /// <summary>
    /// Validate <see cref="Patent"/> instance.
    /// </summary>
    public class PatentValidator : AbstractValidator<Patent>
    {
        /// <summary>
        /// Initialize a <see cref="PatentValidator"/> instance.
        /// </summary>
        public PatentValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(ValidateMassages.NameRequared);
            RuleFor(m => m.CountPage).NotEmpty().WithMessage(ValidateMassages.CountPageRequared);
            RuleFor(m => m.Autor).NotEmpty().WithMessage(ValidateMassages.PatentAuthorRequared);
            RuleFor(m => m.RegistrationNumber).NotEmpty().WithMessage(ValidateMassages.PatentRegistrationNumberRequared);
        }
    }
}
