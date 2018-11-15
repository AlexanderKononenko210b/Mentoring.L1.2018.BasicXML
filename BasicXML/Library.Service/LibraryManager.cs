using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Library.Infrastructure.FakeModels;
using Library.Infrastructure.Interfaces;
using Library.Infrastructure.Models;

namespace Library.Service
{
    /// <summary>
    /// Represents a <see cref="LibraryManager"/> class.
    /// </summary>
    public sealed class LibraryManager
    {
        private readonly IValidator<LibraryArtifact> _validator;
        private Dictionary<LibraryArtifact, IEnumerable<string>> _errorsInfo = 
            new Dictionary<LibraryArtifact, IEnumerable<string>>();

        /// <summary>
        /// Initialize a <see cref="LibraryManager"/> instance.
        /// </summary>
        /// <param name="validator">The validator.</param>
        public LibraryManager(IValidator<LibraryArtifact> validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Gets or sets the errors info.
        /// </summary>
        public IEnumerator ErrorsInfo => _errorsInfo.GetEnumerator();

        /// <summary>
        /// Write information about library artifacts in XML file.
        /// </summary>
        /// <param name="data">The data for write.</param>
        /// <param name="stream">The stream.</param>
        public void Write(IEnumerable<LibraryArtifact> data, Stream stream)
        {
            XmlDocument document = new XmlDocument();

            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-16", string.Empty);
            document.AppendChild(declaration);

            XmlElement root = document.CreateElement("catalogInfo");
            root.SetAttribute("loadTime", $"{DateTime.Now}");
            root.SetAttribute("library", "Special library");

            if (_errorsInfo.Any())
            {
                _errorsInfo.Clear();
            }

            foreach (var artifact in data)
            {
                switch (artifact)
                {
                    case Book item:
                    {
                        var bookVidateResult = _validator.Validate(item);

                        if (!bookVidateResult.isValid)
                        {
                            _errorsInfo.Add(item, bookVidateResult.errors);
                        }
                        else
                        {
                            this.WriteBookElement(item, document, root);
                        }

                        break;
                    }
                        
                    case Newspaper item:
                        var newspaperValidateResult = _validator.Validate(item);

                        if (!newspaperValidateResult.isValid)
                        {
                            _errorsInfo.Add(item, newspaperValidateResult.errors);
                        }
                        else
                        {
                            this.WriteNewspaperElement(item, document, root);
                        }

                        break;
                    case Patent item:
                        var patentValidateResult = _validator.Validate(item);

                        if (!patentValidateResult.isValid)
                        {
                            _errorsInfo.Add(item, patentValidateResult.errors);
                        }
                        else
                        {
                            this.WritePatentElement(item, document, root);
                        }

                        break;
                    default:
                        continue;
                }
            }

            document.AppendChild(root);
            document.Save(stream);
        }

        /// <summary>
        /// Read data from input stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The <see cref="IEnumerable{LibraryArtifact}"/></returns>
        public IEnumerable<LibraryArtifact> Read(Stream stream)
        {
            var reader = XmlReader.Create(stream);
            reader.ReadToFollowing("catalogInfo");
            reader.ReadStartElement();

            while (reader.ReadToNextSibling("artifact"))
            {
                var artifact = XNode.ReadFrom(reader) as XElement;

                switch (artifact.FirstAttribute.Value)
                {
                    case "book":
                    {
                        var book = ReadBookElement(artifact);
                        var validateResult = _validator.Validate(book);

                        if (!validateResult.isValid)
                        {
                            _errorsInfo.Add(book, validateResult.errors);
                        }
                        else
                        {
                            yield return book;
                        }

                        break;
                    }

                    case "newspaper":
                    {
                        var newspaper = ReadNewspaperElement(artifact);
                        var validateResult = _validator.Validate(newspaper);

                        if (!validateResult.isValid)
                        {
                            _errorsInfo.Add(newspaper, validateResult.errors);
                        }
                        else
                        {
                            yield return newspaper;
                        }

                        break;
                    }

                    case "patent":
                    {
                        var patent = ReadPatentElement(artifact);
                        var validateResult = _validator.Validate(patent);

                        if (!validateResult.isValid)
                        {
                            _errorsInfo.Add(patent, validateResult.errors);
                        }
                        else
                        {
                            yield return patent;
                        }

                        break;
                    }

                    default:
                        var notLibraryArtifact = new NotLibraryArtifact();
                        _errorsInfo.Add(notLibraryArtifact, _validator.Validate(notLibraryArtifact).errors);
                        break;
                }
            }
        }

        #region Private write helper methods

        /// <summary>
        /// Write base data in artifact.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="document">The document.</param>
        /// <param name="artifact">The artifact.</param>
        private void WriteBaseData(LibraryArtifact data, XmlDocument document, XmlElement artifact)
        {
            var name = document.CreateElement("name");
            name.InnerText = data.Name;
            artifact.AppendChild(name);

            var countPage = document.CreateElement("countPage");
            countPage.InnerText = data.CountPage.ToString();
            artifact.AppendChild(countPage);

            var note = document.CreateElement("note");
            note.InnerText = data.Note;
            artifact.AppendChild(note);
        }

        /// <summary>
        /// Write book element.
        /// </summary>
        /// <param name="data">The <see cref="Book"/>.</param>
        /// <param name="document">The document.</param>
        /// <param name="root">The root element.</param>
        private void WriteBookElement(Book data, XmlDocument document, XmlElement root)
        {
            var artifact = document.CreateElement("artifact");
            artifact.SetAttribute("item", "book");

            WriteBaseData(data, document, artifact);

            var isbn = document.CreateElement("isbn");
            isbn.InnerText = data.ISBN;
            artifact.AppendChild(isbn);

            var autors = document.CreateElement("authors");
            autors.InnerText = data.Autors;
            artifact.AppendChild(autors);

            var publisherCity = document.CreateElement("publisherCity");
            publisherCity.InnerText = data.PublisherCity;
            artifact.AppendChild(publisherCity);

            var publisherName = document.CreateElement("publisherName");
            publisherName.InnerText = data.PublisherName;
            artifact.AppendChild(publisherName);

            var year = document.CreateElement("year");
            year.InnerText = data.Year.ToString();
            artifact.AppendChild(year);

            root.AppendChild(artifact);
        }

        /// <summary>
        /// Write newspaper element.
        /// </summary>
        /// <param name="data">The <see cref="Newspaper"/> instance.</param>
        /// <param name="document">The document.</param>
        /// <param name="root">The root element.</param>
        private void WriteNewspaperElement(Newspaper data, XmlDocument document, XmlElement root)
        {
            var artifact = document.CreateElement("artifact");
            artifact.SetAttribute("item", "newspaper");

            WriteBaseData(data, document, artifact);

            var issn = document.CreateElement("issn");
            issn.InnerText = data.ISSN;
            artifact.AppendChild(issn);

            var publisherCity = document.CreateElement("publisherCity");
            publisherCity.InnerText = data.PublisherCity;
            artifact.AppendChild(publisherCity);

            var publisherName = document.CreateElement("publisherName");
            publisherName.InnerText = data.PublisherName;
            artifact.AppendChild(publisherName);

            var year = document.CreateElement("year");
            year.InnerText = data.Year.ToString();
            artifact.AppendChild(year);

            var number = document.CreateElement("number");
            number.InnerText = data.Number.ToString();
            artifact.AppendChild(number);

            var publicationDate = document.CreateElement("publicationDate");
            publicationDate.InnerText = data.PublicationDate.ToString("MM-dd-yyyy");
            artifact.AppendChild(publicationDate);

            root.AppendChild(artifact);
        }

        /// <summary>
        /// Write patent element.
        /// </summary>
        /// <param name="data">The <see cref="Patent"/> instance.</param>
        /// <param name="document">The document.</param>
        /// <param name="root">The root element.</param>
        private void WritePatentElement(Patent data, XmlDocument document, XmlElement root)
        {
            var artifact = document.CreateElement("artifact");
            artifact.SetAttribute("item", "patent");

            WriteBaseData(data, document, artifact);

            var autor = document.CreateElement("author");
            autor.InnerText = data.Autor;
            artifact.AppendChild(autor);

            var country = document.CreateElement("country");
            country.InnerText = data.Country;
            artifact.AppendChild(country);

            var registrationNumber = document.CreateElement("registrationNumber");
            registrationNumber.InnerText = data.RegistrationNumber;
            artifact.AppendChild(registrationNumber);

            var requestDate = document.CreateElement("requestDate");
            requestDate.InnerText = data.RequestDate.AddMonths(-1).ToString("MM-dd-yyyy");
            artifact.AppendChild(requestDate);

            var publicationDate = document.CreateElement("publicationDate");
            publicationDate.InnerText = data.PublicationDate.ToString("MM-dd-yyyy");
            artifact.AppendChild(publicationDate);

            root.AppendChild(artifact);
        }

        #endregion

        #region Private read helper methods

        /// <summary>
        /// Read book element.
        /// </summary>
        /// <param name="artifact">The book element.</param>
        /// <returns>The <see cref="Book"/></returns>
        private Book ReadBookElement(XElement artifact)
        {
            var book = new Book();

            book.Name = artifact.Element("name").Value;
            book.CountPage = XmlConvert.ToInt32(artifact.Element("countPage").Value);
            book.Note = artifact.Element("note").Value;
            book.ISBN = artifact.Element("isbn").Value;
            book.Autors = artifact.Element("authors").Value;
            book.PublisherCity = artifact.Element("publisherCity").Value;
            book.PublisherName = artifact.Element("publisherName").Value;
            book.Year = XmlConvert.ToInt32(artifact.Element("year").Value);

            return book;
        }

        /// <summary>
        /// Read newspaper element.
        /// </summary>
        /// <param name="artifact">The newspaper element.</param>
        /// <returns>The <see cref="Newspaper"/></returns>
        private Newspaper ReadNewspaperElement(XElement artifact)
        {
            var newspaper = new Newspaper();

            newspaper.Name = artifact.Element("name").Value;
            newspaper.CountPage = XmlConvert.ToInt32(artifact.Element("countPage").Value);
            newspaper.Note = artifact.Element("note").Value;
            newspaper.ISSN = artifact.Element("issn").Value;
            newspaper.PublisherCity = artifact.Element("publisherCity").Value;
            newspaper.PublisherName = artifact.Element("publisherName").Value;
            newspaper.Year = XmlConvert.ToInt32(artifact.Element("year").Value);
            newspaper.Number = XmlConvert.ToInt32(artifact.Element("number").Value);
            newspaper.PublicationDate = DateTime.ParseExact(artifact.Element("publicationDate").Value, "MM-dd-yyyy", CultureInfo.InvariantCulture);

            return newspaper;
        }

        /// <summary>
        /// Read patent element.
        /// </summary>
        /// <param name="artifact">The patent element.</param>
        /// <returns>The <see cref="Patent"/></returns>
        private Patent ReadPatentElement(XElement artifact)
        {
            var patent = new Patent();

            patent.Name = artifact.Element("name").Value;
            patent.CountPage = XmlConvert.ToInt32(artifact.Element("countPage").Value);
            patent.Note = artifact.Element("note").Value;
            patent.Autor = artifact.Element("author").Value;
            patent.Country = artifact.Element("country").Value;
            patent.RegistrationNumber = artifact.Element("registrationNumber").Value;
            patent.RequestDate = DateTime.ParseExact(artifact.Element("requestDate").Value, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            patent.PublicationDate = DateTime.ParseExact(artifact.Element("publicationDate").Value, "MM-dd-yyyy", CultureInfo.InvariantCulture);

            return patent;
        }

        #endregion
    }
}
