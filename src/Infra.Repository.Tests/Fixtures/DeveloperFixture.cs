using Bogus;
using PapoDeDev.TDD.Domain.Core.Entity;

namespace PapoDeDev.TDD.Infra.Repository.Tests.Fixtures
{
    public class DeveloperFixture
    {
        public Developer CreateValid()
        {
            return new Faker<Developer>("pt_BR")
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .Generate();
        }
    }
}
