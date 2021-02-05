using System;

namespace PapoDeDev.TDD.Domain.Core.Entity
{
    public class Developer : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}