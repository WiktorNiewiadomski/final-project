using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Models.Training;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Tests
{
    public class TrainingServiceTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly ApplicationDbContext _context;
        private readonly TrainingService _service;
        private readonly Mock<IGroupService> _groupServiceMock;
        private readonly Mock<IMemberService> _memberServiceMock;

        public TrainingServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(_dbContextOptions);

            _groupServiceMock = new Mock<IGroupService>();
            _memberServiceMock = new Mock<IMemberService>();

            _service = new TrainingService(_context, _groupServiceMock.Object, _memberServiceMock.Object);
        }

        [Fact]
        public void Create_ShouldAddNewTraining()
        {
            var group = new Group { Id = 1, Name = "TestGroup", Description = "Description", CoachId = 1 };
            _context.Groups.Add(group);
            _context.SaveChanges();

            var coach = new Member { Id = 1, CoachGroups = new List<Group> { group }.ToHashSet() };
            _memberServiceMock.Setup(m => m.GetById(group.CoachId)).Returns(coach);

            _groupServiceMock.Setup(g => g.GetById(group.Id)).Returns(group);

            var dto = new CreateTrainingDto
            {
                GroupId = group.Id,
                TrainingStart = DateTime.UtcNow.AddHours(1),
                TrainingEnd = DateTime.UtcNow.AddHours(2),
                PreNotes = "Pre notes",
                PostNotes = "Post notes"
            };

            var result = _service.Create(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.GroupId, result.GroupId);
            Assert.Equal(dto.TrainingStart, result.TrainingStart);
            Assert.Equal(dto.TrainingEnd, result.TrainingEnd);
            Assert.Equal(dto.PreNotes, result.PreNotes);
            Assert.Equal(dto.PostNotes, result.PostNotes);
        }

        [Fact]
        public void DeleteById_ShouldRemoveTraining()
        {
            var training = new Training { Id = 1, GroupId = 1, TrainingStart = DateTime.UtcNow, TrainingEnd = DateTime.UtcNow.AddHours(1) };
            _context.Trainings.Add(training);
            _context.SaveChanges();

            _service.DeleteById(training.Id);

            Assert.Null(_context.Trainings.Find(training.Id));
        }

        [Fact]
        public void GetById_ShouldReturnTraining()
        {
            var coach = new Member { Id = 1, Name = "Coach", Password = "Pass", Type = Domain.Enums.MemberType.Coach };
            var group = new Group { Id = 1, Name = "TestGroup", Description = "Description", CoachId = 1 };
            var training = new Training { Id = 1, GroupId = group.Id, TrainingStart = DateTime.UtcNow, TrainingEnd = DateTime.UtcNow.AddHours(1) };
            _context.Members.Add(coach);
            _context.Groups.Add(group);
            _context.Trainings.Add(training);
            _context.SaveChanges();

            var result = _service.GetById(training.Id);

            Assert.NotNull(result);
            Assert.Equal(training.Id, result.Id);
            Assert.Equal(group.Id, result.Group.Id);
        }

        [Fact]
        public void GetAll_ShouldReturnAllTrainings()
        {
            var coach = new Member { Id = 1, Name="Coach", Password = "Pass", Type = Domain.Enums.MemberType.Coach };
            var group = new Group { Id = 1, Name = "TestGroup", Description = "Description", CoachId = 1 };
            var trainings = new List<Training> {
                new Training { Id = 1, GroupId = group.Id, TrainingStart = DateTime.UtcNow, TrainingEnd = DateTime.UtcNow.AddHours(1) },
                new Training { Id = 2, GroupId = group.Id, TrainingStart = DateTime.UtcNow.AddHours(1), TrainingEnd = DateTime.UtcNow.AddHours(2) }
            };
            _context.Members.Add(coach);
            _context.Groups.Add(group);
            _context.Trainings.AddRange(trainings);
            _context.SaveChanges();

            var result = _service.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Update_ShouldModifyTraining()
        {
            var coach = new Member { Id = 1, Name = "Coach", Password = "Pass", Type = Domain.Enums.MemberType.Coach };
            var group = new Group { Id = 1, Name = "TestGroup", Description = "Description", CoachId = 1 };
            var training = new Training { Id = 1, GroupId = group.Id, TrainingStart = DateTime.UtcNow, TrainingEnd = DateTime.UtcNow.AddHours(1) };
            _context.Members.Add(coach);
            _context.Groups.Add(group);
            _context.Trainings.Add(training);
            _context.SaveChanges();

            var dto = new UpdateTrainingDto
            {
                TrainingStart = DateTime.UtcNow.AddHours(2),
                TrainingEnd = DateTime.UtcNow.AddHours(3),
                PreNotes = "Updated Pre Notes",
                PostNotes = "Updated Post Notes",
                GroupId = group.Id
            };

            var result = _service.Update(training.Id, dto);

            Assert.NotNull(result);
            Assert.Equal(dto.TrainingStart, result.TrainingStart);
            Assert.Equal(dto.TrainingEnd, result.TrainingEnd);
            Assert.Equal(dto.PreNotes, result.PreNotes);
            Assert.Equal(dto.PostNotes, result.PostNotes);
            Assert.Equal(dto.GroupId, result.GroupId);
        }

        [Fact]
        public void Create_ShouldThrowBadRequestException_WhenCoachHasOverlappingTrainings()
        {
            var group = new Group { Id = 1, Name = "TestGroup", Description = "Description", CoachId = 1 };
            var coach = new Member { Id = 1, CoachGroups = new List<Group> { group }.ToHashSet() };
            _groupServiceMock.Setup(g => g.GetById(group.Id)).Returns(group);
            _memberServiceMock.Setup(m => m.GetById(coach.Id)).Returns(coach);

            var existingTraining = new Training
            {
                Id = 1,
                GroupId = group.Id,
                TrainingStart = DateTime.UtcNow,
                TrainingEnd = DateTime.UtcNow.AddHours(1)
            };
            _context.Trainings.Add(existingTraining);
            _context.SaveChanges();

            var dto = new CreateTrainingDto
            {
                GroupId = group.Id,
                TrainingStart = DateTime.UtcNow.AddMinutes(30),
                TrainingEnd = DateTime.UtcNow.AddHours(1).AddMinutes(30),
                PreNotes = "Pre notes",
                PostNotes = "Post notes"
            };

            var exception = Assert.Throws<BadRequestException>(() => _service.Create(dto));
            Assert.Equal("Coach of this group has other training in that time", exception.Message);
        }

        [Fact]
        public void GetById_ShouldThrowNotFoundException_WhenTrainingDoesNotExist()
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
