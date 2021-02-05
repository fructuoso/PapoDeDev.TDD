using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PapoDeDev.TDD.WebAPI.Developer
{
    public class DeveloperValidator : AbstractValidator<DeveloperModel>
    {
        public DeveloperValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
        }
    }
}
