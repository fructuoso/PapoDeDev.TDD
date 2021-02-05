using AutoMapper;
using PapoDeDev.TDD.Domain.Core.Interfaces.Service;
using PapoDeDev.TDD.WebAPI.Controllers;
using System;
using Entity = PapoDeDev.TDD.Domain.Core.Entity;

namespace PapoDeDev.TDD.WebAPI.Developer
{
    public class DeveloperController : GenericControllerCrud<Guid, Entity.Developer, DeveloperModel>
    {
        public DeveloperController(IServiceCrud<Guid, Entity.Developer> service, IMapper mapper) : base(service, mapper) { }
    }
}
