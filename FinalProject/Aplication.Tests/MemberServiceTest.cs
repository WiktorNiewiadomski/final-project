using Application.Models.Member;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Security;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.Tests
{
    public class MemberServiceTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly Mock<IOptions<JwtConfig>> _mockConfig;
        private readonly JwtConfig _jwtConfig;
        private readonly ApplicationDbContext _context;
        private readonly MemberService _service;

        public MemberServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(_dbContextOptions);

            _jwtConfig = new JwtConfig
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                Key = "TestKeyForJwtTokenTestKeyForJwtToken"
            };

            _mockConfig = new Mock<IOptions<JwtConfig>>();
            _mockConfig.Setup(x => x.Value).Returns(_jwtConfig);

            _service = new MemberService(_context, _mockConfig.Object);
        }

        [Fact]
        public void Create_ShouldAddNewMember()
        {
            var dto = new CreateMemberDto
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1
            };

            var result = _service.Create(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Password, result.Password);
            Assert.Equal(dto.Type, result.Type);
            Assert.Equal(dto.TrainingGroupId, result.TrainingGroupId);
        }

        [Fact]
        public void DeleteById_ShouldRemoveMember()
        {
            var member = new Member
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1
            };

            _context.Members.Add(member);
            _context.SaveChanges();

            _service.DeleteById(member.Id);

            Assert.Null(_context.Members.Find(member.Id));
        }

        [Fact]
        public void GetById_ShouldReturnMember()
        {
            var member = new Member
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1
            };

            _context.Members.Add(member);
            _context.SaveChanges();

            var result = _service.GetById(member.Id);

            Assert.NotNull(result);
            Assert.Equal(member.Name, result.Name);
        }

        [Fact]
        public void GetAll_ShouldReturnAllMembers()
        {
            var members = new List<Member>
            {
                new Member
                {
                    Name = "Test Name 1",
                    Password = "TestPassword1",
                    Type = MemberType.Player,
                    TrainingGroupId = 1
                },
                new Member
                {
                    Name = "Test Name 2",
                    Password = "TestPassword2",
                    Type = MemberType.Player,
                    TrainingGroupId = 2
                }
            };

            _context.Members.AddRange(members);
            _context.SaveChanges();

            var result = _service.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Update_ShouldModifyMember()
        {
            var member = new Member
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1
            };

            _context.Members.Add(member);
            _context.SaveChanges();

            var dto = new UpdateMemberDto
            {
                Name = "Updated Name",
                Password = "UpdatedPassword",
                Type = MemberType.Coach,
                TrainingGroupId = 2
            };

            var result = _service.Update(member.Id, dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Password, result.Password);
            Assert.Equal(dto.Type, result.Type);
            Assert.Equal(dto.TrainingGroupId, result.TrainingGroupId);
        }

        [Fact]
        public void Login_ShouldReturnJwtToken()
        {
            var member = new Member
            {
                Name = "Test Name",
                Password = "TestPassword",
                Type = MemberType.Player,
                TrainingGroupId = 1
            };

            _context.Members.Add(member);
            _context.SaveChanges();

            var dto = new LoginMemberDto
            {
                Name = "Test Name",
                Password = "TestPassword"
            };

            var token = _service.Login(dto);

            Assert.NotNull(token);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
