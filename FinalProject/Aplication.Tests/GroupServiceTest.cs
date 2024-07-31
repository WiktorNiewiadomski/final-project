using Application.Exceptions;
using Application.Models.Group;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class GroupServiceTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly ApplicationDbContext _context;
        private readonly GroupService _service;

        public GroupServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(_dbContextOptions);

            _service = new GroupService(_context);
        }

        [Fact]
        public void Create_ShouldAddNewGroup()
        {
            var dto = new CreateGroupDto
            {
                Name = "Test Group",
                Description = "Test Description",
                Type = GroupType.Active,
                CoachId = 1
            };

            var result = _service.Create(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.Type, result.Type);
            Assert.Equal(dto.CoachId, result.CoachId);
        }

        [Fact]
        public void DeleteById_ShouldRemoveGroup()
        {
            var group = new Group
            {
                Name = "Test Group",
                Description = "Test Description",
                Type = GroupType.Active,
                CoachId = 1
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            _service.DeleteById(group.Id);

            Assert.Null(_context.Groups.Find(group.Id));
        }

        [Fact]
        public void GetById_ShouldReturnGroup()
        {
            var member = new Member
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1,
                Id = 1
            };

            var group = new Group
            {
                Name = "Test Group",
                Description = "Test Description",
                Type = GroupType.Active,
                CoachId = 1
            };

            _context.Members.Add(member);
            _context.Groups.Add(group);
            _context.SaveChanges();

            var result = _service.GetById(group.Id);

            Assert.NotNull(result);
            Assert.Equal(group.Name, result.Name);
        }

        [Fact]
        public void GetAll_ShouldReturnAllGroups()
        {
            var members = new List<Member>
            {
                new Member
                {
                    Name = "Test Name",
                    Password = "TestPassword",
                    Type = MemberType.Player,
                    TrainingGroupId = 1,
                    Id = 1
                },
                new Member
                {
                    Name = "Test Name",
                    Password = "TestPassword",
                    Type = MemberType.Player,
                    TrainingGroupId = 1,
                    Id = 2
                }
            };
            var groups = new List<Group>
            {
                new Group
                {
                    Name = "Test Group 1",
                    Description = "Test Description 1",
                    Type = GroupType.Active,
                    CoachId = 1
                },
                new Group
                {
                    Name = "Test Group 2",
                    Description = "Test Description 2",
                    Type = GroupType.Active,
                    CoachId = 2
                }
            };

            _context.Members.AddRange(members);
            _context.Groups.AddRange(groups);
            _context.SaveChanges();

            var result = _service.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Update_ShouldModifyGroup()
        {
            var member = new Member
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1,
                Id = 1
            };
            var group = new Group
            {
                Name = "Original Name",
                Description = "Original Description",
                Type = GroupType.Active,
                CoachId = 1
            };

            _context.Members.Add(member);
            _context.Groups.Add(group);
            _context.SaveChanges();

            var dto = new UpdateGroupDto
            {
                Name = "Updated Name",
                Description = "Updated Description",
                Type = GroupType.Active,
                CoachId = 2
            };

            var result = _service.Update(group.Id, dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.Type, result.Type);
            Assert.Equal(dto.CoachId, result.CoachId);
        }

        [Fact]
        public void GetById_ShouldThrowNotFoundException_WhenGroupDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _service.GetById(999));

            Assert.Equal("Group not found", exception.Message);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
