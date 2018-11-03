using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Library.Infrastructure;
using Library.Infrastructure.Models;
using Library.Service;
using Library.Service.Interfaces;
using Library.Service.Validators;
using Moq;
using NUnit.Framework;

namespace Library.Test
{
    [TestFixture]
    public class LibraryManagerTest
    {
        private Mock<IValidator<LibraryArtifact>> _mockValidator;
        private string _path;
        
        [SetUp]
        public void Initialize()
        {
            _mockValidator = new Mock<IValidator<LibraryArtifact>>();
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationManager.AppSettings["output"]);
        }

        /// <summary>
        /// Write success.
        /// </summary>
        [Test]
        public void Write_Artifacts_Stream_Dune()
        {
            var listError = new List<string>();
            var countInputArtifacts = 1000;

            _mockValidator.Setup(mock => mock.Validate(It.IsAny<LibraryArtifact>()))
                .Returns(() => (true, listError));

            var manager = new LibraryManager(_mockValidator.Object);

            using (var stream = File.Open(_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                manager.Write(ArtifactInitializer.Initializer(countInputArtifacts, 0), stream);
            }

            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var result = ArtifactCounter.CountArtifact(stream).ToArray();

                Assert.AreEqual(countInputArtifacts, result.Length);
            }
        }

        /// <summary>
        /// Read success.
        /// </summary>
        [Test]
        public void Read_Artifacts_Stream_Dune()
        {
            var listError = new List<string>();
            var countInputArtifacts = 1000;

            _mockValidator.Setup(mock => mock.Validate(It.IsAny<LibraryArtifact>()))
                .Returns(() => (true, listError));

            var manager = new LibraryManager(_mockValidator.Object);

            using (var stream = File.Open(_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                manager.Write(ArtifactInitializer.Initializer(countInputArtifacts, 0), stream);
            }

            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var result = ArtifactCounter.CountArtifact(stream).ToArray();

                Assert.AreEqual(countInputArtifacts, result.Length);
            }
        }

        /// <summary>
        /// Write success only valid artifacts.
        /// </summary>
        [Test]
        public void Write_Artifacts_Stream_Write_Without_NotLibraryArtifacts()
        {
            var validArtifacts = 1000;
            var notValidArtifacts = 10;

            var validator = new ArtifactValidator();

            var manager = new LibraryManager(validator);

            using (var stream = File.Open(_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                manager.Write(ArtifactInitializer.Initializer(validArtifacts, notValidArtifacts), stream);
            }

            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var result = manager.Read(stream).ToArray();

                Assert.AreEqual(validArtifacts - notValidArtifacts, result.Length);
            }
        }

        /// <summary>
        /// Read success only valid artifacts.
        /// </summary>
        [Test]
        public void Read_Artifacts_Stream_Read_Without_NotLibraryArtifacts()
        {
            var validArtifacts = 1000;
            var notValidArtifacts = 10;

            var validator = new ArtifactValidator();

            var manager = new LibraryManager(validator);

            using (var stream = File.Open(_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                manager.Write(ArtifactInitializer.Initializer(validArtifacts, notValidArtifacts), stream);
            }

            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var result = manager.Read(stream).ToArray();

                Assert.AreEqual(validArtifacts - notValidArtifacts, result.Length);
            }
        }
    }
}
