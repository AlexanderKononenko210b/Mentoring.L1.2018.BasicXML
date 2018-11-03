namespace Library.Infrastructure.Models
{
    /// <summary>
    /// Represents a model ><see cref="Book"/> class.
    /// </summary>
    public sealed class Book : LibraryArtifact
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ISBN number.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the authors name and surname.
        /// </summary>
        public string Autors { get; set; }

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

        #endregion

        /// <summary>
        /// Override ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Book: Name: {Name} | Count page: {CountPage} | Note: {Note} |" +
                   $"ISBN: {ISBN} | Authors: {Autors} | Publisher city: {PublisherCity} |" +
                   $"Publisher name: {PublisherName} | Year: {Year}";
        }
    }
}
