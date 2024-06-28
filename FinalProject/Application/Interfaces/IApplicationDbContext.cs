using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Member> Members { get; }
        DbSet<Group> Groups { get; }
        DbSet<Training> Trainings { get; }
    }
}
