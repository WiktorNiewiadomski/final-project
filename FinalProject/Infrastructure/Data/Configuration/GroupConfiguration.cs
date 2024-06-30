using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
	public class GroupConfiguration : IEntityTypeConfiguration<Group>
	{
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasOne(g => g.Coach)
                .WithMany(c => c.CoachGroups)
                .HasForeignKey(g => g.CoachId);
        }
    }
}

