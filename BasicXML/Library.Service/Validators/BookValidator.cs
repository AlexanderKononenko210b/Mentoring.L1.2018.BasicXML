using FluentValidation;
using Library.Infrastructure.Models;
using Library.Service.Resources;

namespace Library.Service.Validators
{
    /// <summary>
    /// Validate book instance.
    /// </summary>
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initialize a <see cref="BookValidator"/> class.
        /// </summary>
        public BookValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(ValidateMassages.NameRequared);
            RuleFor(m => m.CountPage).NotEmpty().WithMessage(ValidateMassages.CountPageRequared);
            RuleFor(m => m.ISBN).NotEmpty().WithMessage(ValidateMassages.BookISBNRequared);
            RuleFor(m => m.Autors).NotEmpty().WithMessage(ValidateMassages.BookAuthorsRequared);
        }
    }
}
