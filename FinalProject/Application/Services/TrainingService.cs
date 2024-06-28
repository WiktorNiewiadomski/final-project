using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Training;
using Domain.Entities;

namespace Application.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IGroupRepository _groupRepository;

        public TrainingService(ITrainingRepository trainingRepository, IGroupRepository groupRepository)
        {
            _trainingRepository = trainingRepository;
            _groupRepository = groupRepository;
        }
        public Training Create(CreateTrainingDto dto)
        {
            Group group = _groupRepository.GetById(dto.GroupId);
            List<Group> coachGroups = _groupRepository.GetCoachGroups(group.Coach.Id);
            foreach (Group coachGroup in coachGroups)
            {
                List<Training> inTimeBracketTrainings = _trainingRepository.GetTrainingsInTimeBracketForGroupId(coachGroup.Id, dto.TrainingStart, dto.TrainingEnd);
                if (inTimeBracketTrainings.Count > 0)
                {
                    throw new HttpRequestException("Coach of this group has other training in that time");
                }
            }
            
            return _trainingRepository.Create(dto);
        }

        public void DeleteById(int id)
        {
            _trainingRepository.DeleteById(id);
        }

        public Training GetById(int id)
        {
            return _trainingRepository.GetById(id);
        }

        public List<Training> List()
        {
            return _trainingRepository.List();
        }

        public Training Update(UpdateTrainingDto dto)
        {
            return _trainingRepository.Update(dto);
        }
    }
}
