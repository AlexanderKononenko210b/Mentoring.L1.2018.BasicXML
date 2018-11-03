using System;
using Bogus;
using Library.Infrastructure.Models;

namespace Library.Infrastructure.FakeModels
{
    /// <summary>
    /// Represents a <see cref="FakePatent"/> class.
    /// </summary>
    public class FakePatent : Faker<Patent>
    {
        /// <summary>
        /// Initialize a <see cref="FakePatent"/> instance.
        /// </summary>
        public FakePatent()
        {
            RuleFor(m => m.Name, p => p.Random.Words(3));
            RuleFor(m => m.CountPage, p => p.Random.Int(10, 1000));
            RuleFor(m => m.Note, p => p.Lorem.Sentence());
            RuleFor(m => m.PublicationDate, p => p.Date.Between(new DateTime(2000, 1, 1), new DateTime(2016, 1, 1)));
            RuleFor(m => m.RequestDate, p => p.Date.Between(new DateTime(2000, 1, 1), new DateTime(2016, 1, 1)));
            RuleFor(m => m.Autor, p => p.Name.FullName());
            RuleFor(m => m.Country, p => p.Address.Country());
            RuleFor(m => m.RegistrationNumber, p => p.Random.ReplaceNumbers("#-###-###"));
        }
    }
}
