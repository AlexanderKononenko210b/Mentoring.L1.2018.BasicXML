using System;

namespace Library.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="Newspaper"/> class.
    /// </summary>
    public sealed class Newspaper : LibraryArtifact
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ISSN number.
        /// </summary>
        public string ISSN { get; set; }

        /// <summary>
        /// Gets or sets the publish city.
        /// </summary>
        public string PublisherCity { get; set; }

        /// <summary>
        /// Gets or sets the publisher name
        /// </summary>
        public string PublisherName { get; set; }

        /// <summary>
        /// Gets or sets the year publishing
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the newspaper number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the newspaper publishing date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        #endregion

        /// <summary>
        /// Override ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Newspaper: Name: {Name} | Count page: {CountPage} | Note: {Note} |" +
                   $"ISSN: {ISSN} | Publisher city: {PublisherCity} | Publisher name: {PublisherName} |" +
                   $" Year: {Year} | Number: {Number} | PublicationDate: {PublicationDate:d}";
        }
    }
}
