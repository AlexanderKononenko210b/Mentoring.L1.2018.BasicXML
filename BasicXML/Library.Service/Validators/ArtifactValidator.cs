using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Library.Infrastructure.Models;
using Library.Service.Interfaces;
using Library.Service.Resources;

namespace Library.Service.Validators
{
    /// <summary>
    /// Validator for <see cref="LibraryArtifact"/> class.
    /// </summary>
    public class ArtifactValidator : IValidator<LibraryArtifact>
    {
        /// <inheritdoc/>
        public (bool isValid, IEnumerable<string> errors) Validate(LibraryArtifact instance)
        {
            switch (instance)
            {
                case Book item:
                {
                    var validator = new BookValidator();

                    return ValidateHelper(validator, item);
                }
                case Newspaper item:
                {
                    var validator = new NewspaperValidator();

                    return ValidateHelper(validator, item);
                }
                case Patent item:
                {
                    var validator = new PatentValidator();

                    return ValidateHelper(validator, item);
                }
                    
                default:
                    return (false, new string[]{ ValidateMassages.NotLibraryArtifact});
            }
        }

        /// <summary>
        /// Validate helper.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="Tuple"/>:
        /// isValid - true if input instance is valid otherwase false,
        /// errors - errors collection.
        /// </returns>
        private static (bool isValid, IEnumerable<string> errors) ValidateHelper<T>(FluentValidation.IValidator validator, T item)
        {
            var resultValidate = validator.Validate(item);

            if (!resultValidate.IsValid)
            {
                return(false, GetErrors(resultValidate.Errors));
            }

            return (true, null);
        }

        /// <summary>
        /// Get messages from error info.
        /// </summary>
        /// <param name="errors">The errors after parsing.</param>
        /// <returns>The errors messages.</returns>
        private static IEnumerable<string> GetErrors(IList<ValidationFailure> errors)
        {
            var errorsMessages = new List<string>();

            foreach (var error in errors)
            {
                errorsMessages.Add(error.ErrorMessage);
            }

            return errorsMessages;
        }
    }
}
