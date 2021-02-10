using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Infra.Repository.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PapoDeDev.TDD.Infra.Repository.Tests
{
    public class UnitOfWorkTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly GenericRepositoryCrud<Guid, Developer> _Repository;
        private readonly UnitOfWork _UnitOfWork;

        public UnitOfWorkTests(DependencyInjectionFixture fixture)
        {
            IServiceProvider serviceProvider = fixture._ServiceProvider;

            _Repository = serviceProvider.GetService<GenericRepositoryCrud<Guid, Developer>>();
            _UnitOfWork = serviceProvider.GetService<UnitOfWork>();
        }

        [Fact(DisplayName = "DADO um novo Developer sendo persistido QUANDO o commit é realizado ENTÃO a transação deve ser confirmada")]
        public async Task Commit()
        {
            //Arrange
            Developer developer = new Developer() { FirstName = "Victor", LastName = "Fructuoso" };

            //Act
            await _Repository.AddAsync(developer);
            await _UnitOfWork.CommitAsync();
            Developer developerAdded =  await _Repository.GetAsync(developer.Id);

            //Assert
            Assert.NotNull(developerAdded);
        }

        [Fact(DisplayName = "DADO um novo Developer sendo persistido QUANDO o rollback é realizado ENTÃO a transação deve ser abortada E o Developer não deve ser mantido no repositório")]
        public async Task Rollback()
        {
            //Arrange
            Developer developer = new Developer() { FirstName = "Victor", LastName = "Fructuoso" };

            //Act
            await _Repository.AddAsync(developer);
            await _UnitOfWork.RollbackAsync();
            Developer developerAdded =  await _Repository.GetAsync(developer.Id);

            //Assert
            Assert.Null(developerAdded);
        }

        [Fact(DisplayName = "DADO um Developer sendo modificado em memória QUANDO o update não é executado E o commit é realizado ENTÃO então o Developer não deve ser modificado no repositório")]
        public async Task CommitWithoutChanges()
        {
            //Arrange
            Developer developer = _Repository.GetAll().First();
            string firstName = developer.FirstName;
            developer.FirstName = "Modified";

            //Act
            await _UnitOfWork.CommitAsync();
            Developer developerUnchanged = await _Repository.GetAsync(developer.Id);

            //Assert
            Assert.Equal(firstName, developerUnchanged.FirstName);
        }
    }
}
