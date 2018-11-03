using System;
using System.Collections.Generic;
using Library.Infrastructure.FakeModels;
using Library.Infrastructure.Models;

namespace Library.Infrastructure
{
    /// <summary>
    /// Initialize test artifact instances
    /// </summary>
    public static class ArtifactInitializer
    {
        /// <summary>
        /// Initialize artifacts instances.
        /// </summary>
        /// <param name="numberValidArtifacts">Number valid artifacts.</param>
        /// <param name="numberNotValidArtifact">Number not valid artifacts.</param>
        /// <returns>The <see cref="IEnumerable{LibraryArtifact}"/></returns>
        public static IEnumerable<LibraryArtifact> Initializer(int numberValidArtifacts, int numberNotValidArtifact)
        {
            if (numberValidArtifacts < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberValidArtifacts));
            }

            if (numberNotValidArtifact < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberNotValidArtifact));
            }

            Random rand = new Random();
            var countErrors = 0;

            for (int i = 0; i < numberValidArtifacts; i++)
            {
                int number;

                if (numberNotValidArtifact > 0)
                {
                    number = rand.Next(0, 4);
                }
                else
                {
                    number = rand.Next(0, 3);
                }

                switch (number)
                {
                    case 0:
                        yield return new FakeBook();
                        break;
                    case 1:
                        yield return new FakeNewspaper();
                        break;
                    case 2:
                        yield return new FakePatent();
                        break;
                    case 3:
                        if (countErrors < numberNotValidArtifact)
                        {
                            yield return new NotLibraryArtifact();
                            countErrors++;
                        }
                        else
                        {
                            yield return new FakeBook();
                        }
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
