using AutoMapper;
using Entity = PapoDeDev.TDD.Domain.Core.Entity;

namespace PapoDeDev.TDD.WebAPI.Developer
{
    public class DeveloperMapper : Profile
    {
        public DeveloperMapper()
        {
            CreateMap<Entity.Developer, DeveloperModel>();
            CreateMap<DeveloperModel, Entity.Developer>();
        }
    }
}
