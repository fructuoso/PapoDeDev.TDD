using PapoDeDev.TDD.Domain.Core.Entity;
using PapoDeDev.TDD.Domain.Core.Interfaces.Repository;
using PapoDeDev.TDD.Domain.Core.Interfaces.Service;
using System;

namespace PapoDeDev.TDD.Domain.Services
{
    public class DeveloperService : GenericServiceCrud<Guid, Developer>, IDeveloperService
    {
        public DeveloperService(IRepositoryCrud<Guid, Developer> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork) { }
    }
}
