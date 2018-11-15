using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Library.Test.Helpers
{
    /// <summary>
    /// Represents a <see cref="ArtifactCounter"/> class.
    /// </summary>
    public class ArtifactCounter
    {
        /// <summary>
        /// Count artifacts.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The count artifacts./></returns>
        public static int CountArtifact(Stream stream)
        {
            var reader = XmlReader.Create(stream);
            reader.ReadToFollowing("catalogInfo");
            reader.ReadStartElement();
            var countArtifact = 0;

            while (reader.ReadToNextSibling("artifact"))
            {
                var artifact = XNode.ReadFrom(reader) as XElement;

                countArtifact += artifact == null ? 0 : 1;
            }

            return countArtifact;
        }
    }
}
