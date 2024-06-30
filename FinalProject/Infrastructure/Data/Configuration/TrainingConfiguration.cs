using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
	{
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Group)
                .WithMany(g => g.GroupTrainings)
                .HasForeignKey(t => t.GroupId); 
        }
    }
}

