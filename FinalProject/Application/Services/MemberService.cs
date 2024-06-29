using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Models.Member;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public MemberService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
                .Include(g => g.TrainingGroup)
                .Include(g => g.CoachGroups)
                .First(g => g.Id == id);
            if (found == null)
            {
                throw new NotFoundException("Member not found");
            }

            return found;
        }

        public List<Member> GetAll()
        {
            return _applicationDbContext.Members
                .Include(g => g.TrainingGroup)
                .Include(g => g.CoachGroups)
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
    }
}
