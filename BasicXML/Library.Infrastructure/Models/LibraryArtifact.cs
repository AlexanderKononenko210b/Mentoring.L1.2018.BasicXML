namespace Library.Infrastructure.Models
{
    /// <summary>
    /// Represent abstract <see cref="LibraryArtifact"/> class.
    /// </summary>
    public abstract class LibraryArtifact
    {
        /// <summary>
        /// Gets or sets the artifacts name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the count of pages.
        /// </summary>
        public int CountPage { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note { get; set; }
    }
}
