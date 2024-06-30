using System.Reflection;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Member> Members { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Training> Trainings { get; set; }

        private string DbPath { get; set; }

        public ApplicationDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "club.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = 1,
                    Name = "Właściciel",
                    Password = "haslo123",
                    Type = MemberType.Owner,
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = 2,
                    Name = "Trener",
                    Password = "haslo123",
                    Type = MemberType.Coach,
                }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group()
                {
                    Id = 1,
                    Name = "Grupa słabiaków",
                    Description = "Zawiera samych słabiaków z naszego klubu",
                    Type = GroupType.Fundamental,
                    CoachId = 2,
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = 3,
                    Name = "Zawodnik 1",
                    Password = "haslo123",
                    Type = MemberType.Player,
                    TrainingGroupId = 1
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = 4,
                    Name = "Zawodnik 2",
                    Password = "haslo123",
                    Type = MemberType.Player,
                    TrainingGroupId = 1
                }
            );

            modelBuilder.Entity<Training>().HasData(
                new Training()
                {
                    Id = 1,
                    PreNotes = "Trzeba im wyjaśnic na treningu jak się biega bo nie umieją",
                    PostNotes = "Po wyjaśnieniu jak biegać nadal nic nie ogarniają",
                    GroupId = 1,
                    TrainingStart = new DateTime(2024, 06, 30, 10, 0, 0),
                    TrainingEnd = new DateTime(2024, 06, 30, 12, 0, 0),
                }
            );

            modelBuilder.Entity<Training>().HasData(
                new Training()
                {
                    Id = 2,
                    PreNotes = "Podjąć kolejną próbę nauczenia ich biegać",
                    GroupId = 1,
                    TrainingStart = new DateTime(2024, 07, 30, 10, 0, 0),
                    TrainingEnd = new DateTime(2024, 07, 30, 12, 0, 0),
                }
            );
        }
    }
}
