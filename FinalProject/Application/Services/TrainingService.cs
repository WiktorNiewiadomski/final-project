using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Models.Training;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IGroupService _groupService;
        private readonly IMemberService _memberService;

        public TrainingService(IApplicationDbContext applicationDbContext, IGroupService groupService, IMemberService memberService)
        {
            _applicationDbContext = applicationDbContext;
            _groupService = groupService;
            _memberService = memberService;
        }
        public Training Create(CreateTrainingDto dto)
        {
            var group = _groupService.GetById(dto.GroupId);
            var coach = _memberService.GetById(group.CoachId);
            foreach (var coachGroup in coach.CoachGroups)
            {
                var inTimeBracketTrainings = _applicationDbContext.Trainings
                    .Where(t => dto.TrainingStart < t.TrainingEnd && dto.TrainingEnd > t.TrainingStart)
                    .ToList();
                if (inTimeBracketTrainings.Count > 0)
                {
                    throw new BadRequestException("Coach of this group has other training in that time");
                }
            }

            var newTraining = new Training();
            newTraining.GroupId = dto.GroupId;
            newTraining.TrainingStart = dto.TrainingStart;
            newTraining.TrainingEnd = dto.TrainingEnd;
            newTraining.PreNotes = dto.PreNotes;
            newTraining.PostNotes = dto.PostNotes;
            
            var e = _applicationDbContext.Trainings.Add(newTraining);
            _applicationDbContext.SaveChanges();
            return e.Entity;
        }

        public void DeleteById(int id)
        {
            var deleted = _applicationDbContext.Trainings.Find(id);
            if (deleted != null)
            {
                _applicationDbContext.Trainings.Remove(deleted);
                _applicationDbContext.SaveChanges();
            }
        }

        public Training GetById(int id)
        {
            var found = _applicationDbContext.Trainings
                .Include(t => t.Group)
                .Include(t => t.Group.Coach)
                .FirstOrDefault(t => t.Id == id);
            if (found == null)
            {
                throw new NotFoundException("Group not found");
            }

            return found;
        }

        public List<Training> GetAll()
        {
            return _applicationDbContext.Trainings
                .Include(t => t.Group)
                .Include(t => t.Group.Coach)
                .ToList();
        }

        public Training Update(int id, UpdateTrainingDto dto)
        {
            var updated = GetById(id);
            updated.TrainingStart = (DateTime)(dto.TrainingStart != null ? dto.TrainingStart : updated.TrainingStart);
            updated.TrainingEnd = (DateTime)(dto.TrainingEnd != null ? dto.TrainingEnd : updated.TrainingEnd);
            updated.PreNotes = dto.PreNotes != null ? dto.PreNotes : updated.PreNotes;
            updated.PostNotes = dto.PostNotes != null ? dto.PostNotes : updated.PostNotes;
            // TODO: check for overlaping trainings
            updated.GroupId = (int)(dto.GroupId != null ? dto.GroupId : updated.GroupId);
            var e = _applicationDbContext.Trainings.Update(updated);
            _applicationDbContext.SaveChanges();
            return e.Entity;
        }
    }
}
