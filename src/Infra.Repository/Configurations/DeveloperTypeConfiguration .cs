using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PapoDeDev.TDD.Domain.Core.Entity;

namespace PapoDeDev.TDD.Infra.Repository.Configurations
{
    public class DeveloperTypeConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.FirstName).HasMaxLength(50);
            builder.Property(o => o.LastName).HasMaxLength(50);
        }
    }
}