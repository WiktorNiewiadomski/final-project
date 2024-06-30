using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
	public class MemberConfiguration : IEntityTypeConfiguration<Member>
	{

        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.TrainingGroup)
                .WithMany(g => g.GroupPlayers)
                .HasForeignKey(m => m.TrainingGroupId);
        }
    }
}

