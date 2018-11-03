using System;
using Bogus;
using Library.Infrastructure.Models;

namespace Library.Infrastructure.FakeModels
{
    /// <summary>
    /// Represents a <see cref="FakeNewspaper"/> class.
    /// </summary>
    public class FakeNewspaper : Faker<Newspaper>
    {
        /// <summary>
        /// Initialize a <see cref="FakeNewspaper"/> instance.
        /// </summary>
        public FakeNewspaper()
        {
            RuleFor(m => m.Name, p => p.Random.Words(3));
            RuleFor(m => m.CountPage, p => p.Random.Int(10, 1000));
            RuleFor(m => m.Note, p => p.Lorem.Sentence());
            RuleFor(m => m.ISSN, p => p.Random.ReplaceNumbers("###-###-###"));
            RuleFor(m => m.PublisherCity, p => p.Address.City());
            RuleFor(m => m.PublisherName, p => p.Company.CompanyName());
            RuleFor(m => m.Year, p => p.Random.Int(1900, 2016));
            RuleFor(m => m.PublicationDate, p => p.Date.Between(new DateTime(2000, 1, 1), new DateTime(2016, 1, 1)));
            RuleFor(m => m.Number, p => p.Random.Int(1, 15));
        }
    }
}
