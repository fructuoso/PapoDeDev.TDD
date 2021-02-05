using Moq;
using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PapoDeDev.TDD.Domain.Services.Tests
{
    public class DeveloperServiceTests
    {
        [Fact(DisplayName = "DADO um novo Developer válido QUANDO tentamos persistir ele ENTÃO ele deve ser persistido no repositório")]
        public async Task AddDeveloper()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);

            Developer developer = new Developer();

            //Act
            await _Service.AddAsync(developer);

            //Assert
            _Repository.Verify(o => o.AddAsync(developer));
            _UnitOfWork.Verify(o => o.CommitAsync(default));
        }

        [Fact(DisplayName = "DADO um Developer existente QUANDO tentamos realizar sua excluisão por Id ENTÃO ele deve ser removido do repositório")]
        public async Task DeleteDeveloper()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();
            _Repository.Setup(o => o.DeleteAsync(It.IsAny<Guid>())).Returns<Guid>(id => Task.FromResult(new Developer()));
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);
            Guid id = Guid.NewGuid();

            //Act
            await _Service.DeleteAsync(id);

            //Assert
            _Repository.Verify(o => o.DeleteAsync(id));
            _UnitOfWork.Verify(o => o.CommitAsync(default));
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO tentamos realizar sua excluisão por Id ENTÃO ele não deve ser removido do repositório")]
        public async Task DeleteDeveloperNotExists()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);
            Guid id = Guid.NewGuid();

            //Act
            await _Service.DeleteAsync(id);

            //Assert
            _Repository.Verify(o => o.DeleteAsync(id));
            _UnitOfWork.Verify(o => o.CommitAsync(default), Times.Never);
        }

        [Fact(DisplayName = "DADO um Developer existente QUANDO tentamos realizar uma atualização ENTÃO ele deve ser atualizado do repositório")]
        public async Task UpdateDeveloper()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();
            _Repository.Setup(o => o.UpdateAsync(It.IsAny<Developer>())).Returns<Developer>(developer => Task.FromResult(developer));
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);
            Developer developer = new Developer();

            //Act
            await _Service.UpdateAsync(developer);

            //Assert
            _Repository.Verify(o => o.UpdateAsync(developer));
            _UnitOfWork.Verify(o => o.CommitAsync(default));
        }

        [Fact(DisplayName = "DADO um Developer inexistente QUANDO tentamos realizar uma atualização ENTÃO ele não deve ser atualizado do repositório")]
        public async Task UpdateDeveloperNotExists()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);
            Developer developer = new Developer();

            //Act
            await _Service.UpdateAsync(developer);

            //Assert
            _Repository.Verify(o => o.UpdateAsync(developer));
            _UnitOfWork.Verify(o => o.CommitAsync(default), Times.Never);
        }

        [Fact(DisplayName = "DADO um Developer existente QUANDO tentamos realizar uma consulta por Id ENTÃO ele deve ser retornado")]
        public async Task GetDeveloper()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();

            _Repository
                .Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns<Guid>(o => Task.FromResult(new Developer() { Id = o }));

            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();

            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);

            //Act
            Developer developer = await _Service.GetAsync(id);

            //Assert
            _Repository.Verify(o => o.GetAsync(id));
            Assert.Equal(id, developer.Id);
        }

        [Fact(DisplayName = "DADO que temos ao menos um Developer armazenado no repositório QUANDO tentamos realizar uma listagem ENTÃO todos devem ser retornados")]
        public void GetAllDeveloperExists()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Developer>> _Repository = new Mock<IRepositoryCrud<Guid, Developer>>();

            _Repository.Setup(o => o.GetAll()).Returns(new List<Developer>() { new Developer(), new Developer(), new Developer() });

            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();

            DeveloperService _Service = new DeveloperService(_Repository.Object, _UnitOfWork.Object);

            //Act
            IEnumerable<Developer> developers = _Service.GetAll();

            //Assert
            _Repository.Verify(o => o.GetAll());
            Assert.NotNull(developers);
            Assert.NotEmpty(developers);
            Assert.Equal(3, developers.Count());
        }
    }
}