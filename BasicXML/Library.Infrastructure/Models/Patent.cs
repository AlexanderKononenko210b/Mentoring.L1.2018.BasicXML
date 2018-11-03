using System;

namespace Library.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="Patent"/> class.
    /// </summary>
    public sealed class Patent : LibraryArtifact
    {
        #region Properties

        /// <summary>
        /// Gets or sets the authors name and surname.
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// Gets or sets the patents country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the registration number.
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        #endregion

        /// <summary>
        /// Override ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Patent: Name: {Name} | Count page: {CountPage} | Note: {Note} |" +
                   $"Author: {Autor} | Country: {Country} | Registration number: {RegistrationNumber} |" +
                   $" Request date: {RequestDate:d} | Publication date: {PublicationDate:d}";
        }
    }
}
