using System;
using System.Configuration;
using System.IO;
using Library.Infrastructure;
using Library.Service;
using Library.Service.Validators;

namespace ConsoleLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationManager.AppSettings["output"]);

            var validator = new ArtifactValidator();
            var manager = new LibraryManager(validator);

            using (var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                manager.Write(ArtifactInitializer.Initializer(1000, 0), stream);
            }

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                foreach (var artifact in manager.Read(stream))
                {
                    Console.WriteLine(artifact.ToString());
                }
            }
        }
    }
}
