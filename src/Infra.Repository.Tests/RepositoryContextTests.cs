using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Infra.Repository.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PapoDeDev.TDD.Infra.Repository.Tests
{
    public class RepositoryContextTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly GenericRepositoryCrud<Guid, Developer> _Repository;
        private readonly UnitOfWork _UnitOfWork;

        public RepositoryContextTests(DependencyInjectionFixture fixture)
        {
            IServiceProvider serviceProvider = fixture._ServiceProvider;

            _Repository = serviceProvider.GetService<GenericRepositoryCrud<Guid, Developer>>();
            _UnitOfWork = serviceProvider.GetService<UnitOfWork>();
        }

        [Fact(DisplayName = "DADO um Developer QUANDO o mesmo for recuperado e modificado 2 vezes ENTÃO considerar a segunda modificação ao realizar o Commit")]
        public async Task CommitWithoutChanges()
        {
            //Arrange
            Guid id = _Repository.GetAll().First().Id;

            //Act
            Developer developer1 = await _Repository.GetAsync(id);
            developer1.FirstName = "First Change";
            await _Repository.UpdateAsync(developer1);
            
            Developer developer2 = await _Repository.GetAsync(id);
            developer2.FirstName = "Second Change";
            await _Repository.UpdateAsync(developer2);

            await _UnitOfWork.CommitAsync();
            Developer developer = await _Repository.GetAsync(id);

            //Assert
            Assert.Equal(developer2.FirstName, developer.FirstName);
        }
    }
}
