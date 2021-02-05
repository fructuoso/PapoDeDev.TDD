using PapoDeDev.TDD.Domain.Core.Entity;
using System;

namespace PapoDeDev.TDD.Domain.Core.Interfaces.Service
{
    public interface IDeveloperService : IServiceCrud<Guid, Developer> { }
}
