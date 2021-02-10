using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using PapoDeDev.TDD.Infra.Repository.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PapoDeDev.TDD.Infra.Repository.Tests
{
    public class GenericRepositoryCrudTests : IClassFixture<DependencyInjectionFixture>,
                                              IClassFixture<DeveloperFixture>
    {
        private readonly GenericRepositoryCrud<Guid, Developer> _Repository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly RepositoryContext _Context;
        private readonly DeveloperFixture _DeveloperFixture;

        public GenericRepositoryCrudTests(DependencyInjectionFixture dependencyInjectionFixture, DeveloperFixture developerFixture)
        {
            IServiceProvider serviceProvider = dependencyInjectionFixture._ServiceProvider;

            _Repository = serviceProvider.GetService<GenericRepositoryCrud<Guid, Developer>>();
            _UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _Context = serviceProvider.GetService<RepositoryContext>();

            _DeveloperFixture = developerFixture;
        }

        [Fact(DisplayName = "DADO um Developer QUANDO o mesmo for persistido no repositório ENTÃO um Id deve ser gerado automaticamente")]
        public async Task AddDeveloper()
        {
            //Arrange
            Developer developer = _DeveloperFixture.CreateValid();
            
            //Act
            await _Repository.AddAsync(developer);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.NotNull(developer);
            Assert.NotEqual(default, developer.Id);
        }
        
        [Fact(DisplayName = "DADO um Developer existente QUANDO remover o mesmo do repositório ENTÃO o Developer deve ser retornado")]
        public async Task DeleteDeveloperExists()
        {
            //Arrange
            Guid id = _Context.Set<Developer>().Select(o => o.Id).First();

            //Act
            Developer developer = await _Repository.DeleteAsync(id);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.NotNull(developer);
            Assert.Equal(id, developer.Id);
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO remover o mesmo do repositório ENTÃO retornar NULL")]
        public async Task DeleteDeveloperNotExists()
        {
            //Arrange
            Guid id = Guid.Parse("c87c6eb2-715f-4553-be17-16bfbdd21725");

            //Act
            Developer developer = await _Repository.DeleteAsync(id);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.Null(developer);
        }

        [Fact(DisplayName = "DADO um Developer existente QUANDO realizar uma consulta por Id ENTÃO o Developer deve ser retornado")]
        public async Task GetDeveloperExists()
        {
            //Arrange
            Guid id = _Context.Set<Developer>().Select(o => o.Id).First();

            //Act
            Developer developer = await _Repository.GetAsync(id);

            //Assert
            Assert.NotNull(developer);
            Assert.Equal(id, developer.Id);
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO realizar uma consulta por Id ENTÃO retornar NULL")]
        public async Task GetDeveloperNotExists()
        {
            //Arrange
            Guid id = Guid.Parse("c87c6eb2-715f-4553-be17-16bfbdd21725");

            //Act
            Developer developer = await _Repository.GetAsync(id);

            //Assert
            Assert.Null(developer);
        }

        [Fact(DisplayName = "DADO que temos ao menos um Developer no repositório QUANDO realizar uma listagem ENTÃO todos os registros devem ser retornados")]
        public void GetAllDeveloperExists()
        {
            //Arrange

            //Act
            var developers = _Repository.GetAll();

            //Assert
            Assert.NotNull(developers);
            Assert.NotEmpty(developers);
        }

        [Fact(DisplayName = "DADO um Developer existente QUANDO atualizar o FirstName ENTÃO o Developer deve ser retornado")]
        public async Task UpdateDeveloperExists()
        {
            //Arrange
            Guid id = _Context.Set<Developer>().Select(o => o.Id).First();

            //Act
            Developer developer = await _Repository.GetAsync(id);
            developer.FirstName = $"{developer.FirstName} (MODIFICADO)";

            Developer developerUpdated = await _Repository.UpdateAsync(developer);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.NotNull(developerUpdated);
            Assert.Equal(developer.FirstName, developerUpdated.FirstName);
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO atualizar o mesmo no repositório ENTÃO retornar NULL")]
        public async Task UpdateDeveloperNotExists()
        {
            //Arrange
            Developer developer = new Developer { Id = Guid.NewGuid() };

            //Act
            Developer developerUpdated = await _Repository.UpdateAsync(developer);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.Null(developerUpdated);
        }
    }
}
