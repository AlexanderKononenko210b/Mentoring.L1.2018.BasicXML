using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Library.Infrastructure
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
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<int> CountArtifact(Stream stream)
        {
            var reader = XmlReader.Create(stream);
            reader.ReadToFollowing("catalogInfo");
            reader.ReadStartElement();

            while (reader.ReadToNextSibling("artifact"))
            {
                var artifact = XNode.ReadFrom(reader) as XElement;

                yield return artifact == null ? 0 : 1;
            }
        }
    }
}
