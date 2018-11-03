using System;
using System.Collections.Generic;
using Library.Infrastructure.Models;

namespace Library.Service.Interfaces
{
    /// <summary>
    /// Represents an <see cref="IValidator{T}"/> interface.
    /// </summary>
    public interface IValidator<in T>
        where T : LibraryArtifact
    {
        /// <summary>
        /// Validate input instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The <see cref="Tuple"/>:
        /// isValid - true if input instance is valid otherwise false,
        /// errors - errors message collection.
        /// </returns>
        (bool isValid, IEnumerable<string> errors) Validate(T instance);
    }
}
