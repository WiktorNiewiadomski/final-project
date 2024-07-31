using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Models.Group;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GroupService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Group Create(CreateGroupDto dto)
        {
            Group newGroup = new Group();
            newGroup.Name = dto.Name;
            newGroup.Description = dto.Description;
            newGroup.Type = dto.Type;
            newGroup.CoachId = dto.CoachId;
            var e = _applicationDbContext.Groups.Add(newGroup);
            _applicationDbContext.SaveChanges();
            return e.Entity;
        }

        public void DeleteById(int id)
        {
            var deleted = _applicationDbContext.Groups.Find(id);
            if (deleted != null)
            {
                _applicationDbContext.Groups.Remove(deleted);
                _applicationDbContext.SaveChanges();
            }
        }

        public Group GetById(int id)
        {
            var found = _applicationDbContext.Groups
                .Include(g => g.Coach)
                .Include(g => g.GroupTrainings)
                .FirstOrDefault(g => g.Id == id);
            if (found == null)
            {
                throw new NotFoundException("Group not found");
            }

            return found;
        }

        public List<Group> GetAll()
        {
            return _applicationDbContext.Groups
                .Include(g => g.Coach)
                .Include(g => g.GroupTrainings)
                .ToList();
        }

        public Group Update(int id, UpdateGroupDto dto)
        {
            var updated = GetById(id);
            updated.Name = dto.Name != null ? dto.Name : updated.Name;
            updated.Description = dto.Description != null ? dto.Description : updated.Description;
            updated.Type = (Domain.Enums.GroupType)(dto.Type != null ? dto.Type : updated.Type);
            // TODO: add some check if trainings of group new coach are not overlaping
            updated.CoachId = (int)(dto.CoachId != null ? dto.CoachId : updated.CoachId);
            var e = _applicationDbContext.Groups.Update(updated);
            _applicationDbContext.SaveChanges();
            return e.Entity;
        }
    }
}
