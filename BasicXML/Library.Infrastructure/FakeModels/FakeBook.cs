using Bogus;
using Library.Infrastructure.Models;

namespace Library.Infrastructure.FakeModels
{
    /// <summary>
    /// Represents a <see cref="FakeBook"/> class.
    /// </summary>
    public class FakeBook : Faker<Book>
    {
        /// <summary>
        /// Initialize a new <see cref="FakeBook"/> instance.
        /// </summary>
        public FakeBook()
        {
            RuleFor(m => m.Name, p => p.Random.Words(3));
            RuleFor(m => m.CountPage, p => p.Random.Int(10, 1000));
            RuleFor(m => m.Note, p => p.Lorem.Sentence());
            RuleFor(m => m.Autors, p => $"{p.Person.FullName}; {p.Person.FullName}; {p.Person.FullName}");
            RuleFor(m => m.ISBN, p => p.Random.ReplaceNumbers("###-###-###"));
            RuleFor(m => m.PublisherCity, p => p.Address.City());
            RuleFor(m => m.PublisherName, p => p.Company.CompanyName());
            RuleFor(m => m.Year, p => p.Random.Int(1900, 2016));
        }
    }
}
