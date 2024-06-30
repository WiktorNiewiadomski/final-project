using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Models.Member;
using Domain.Entities;
using Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IOptions<JwtConfig> _config;

        public MemberService(IApplicationDbContext applicationDbContext, IOptions<JwtConfig> config)
        {
            _applicationDbContext = applicationDbContext;
            _config = config;
        }

        public Member Create(CreateMemberDto dto)
        {
            var newMember = new Member();
            newMember.Name = dto.Name;
            newMember.Password = dto.Password;
            newMember.Type = dto.Type;
            newMember.TrainingGroupId = dto.TrainingGroupId;
            var e = _applicationDbContext.Members.Add(newMember);
            _applicationDbContext.SaveChanges();
            return e.Entity;
        }

        public void DeleteById(int id)
        {
            var deleted = _applicationDbContext.Members.Find(id);
            if (deleted != null)
            {
                _applicationDbContext.Members.Remove(deleted);
                _applicationDbContext.SaveChanges();
            }
        }

        public Member GetById(int id)
        {
            var found = _applicationDbContext.Members
                .Include(m => m.TrainingGroup)
                .Include(m => m.CoachGroups)
                .First(m => m.Id == id);
            if (found == null)
            {
                throw new NotFoundException("Member not found");
            }

            return found;
        }

        public List<Member> GetAll()
        {
            return _applicationDbContext.Members
                .Include(m => m.TrainingGroup)
                .Include(m => m.CoachGroups)
                .ToList();
        }

        public Member Update(int id, UpdateMemberDto dto)
        {
            var updated = GetById(id);
            updated.Name = dto.Name != null ? dto.Name : updated.Name;
            updated.Password = dto.Password != null ? dto.Password : updated.Password;
            // TODO: if changing type check if can if is coach or temove from training group
            updated.Type = (Domain.Enums.MemberType)(dto.Type != null ? dto.Type : updated.Type);
            // TODO: add some check if trainings of group new coach are not overlaping
            updated.TrainingGroupId = (int)(dto.TrainingGroupId != null ? dto.TrainingGroupId : updated.TrainingGroupId);
            var e = _applicationDbContext.Members.Update(updated);
            _applicationDbContext.SaveChanges();
            return e.Entity;
        }

        public string Login(LoginMemberDto dto)
        {
            var foundMember = _applicationDbContext.Members.Where(m => m.Name == dto.Name && m.Password == dto.Password).First();
            var issuer = _config.Value.Issuer;
            var audience = _config.Value.Audience;
            var key = Encoding.ASCII.GetBytes(_config.Value.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim("id", foundMember.Id.ToString()),
                 new Claim("name", foundMember.Name),
                 new Claim("type", foundMember.Type.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return tokenHandler.WriteToken(token);
        }
    }
}
