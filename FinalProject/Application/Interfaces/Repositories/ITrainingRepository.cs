using Application.Models.Training;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ITrainingRepository : IBaseRepository<Training, CreateTrainingDto, UpdateTrainingDto>
    {

        List<Training> GetTrainingsInTimeBracketForGroupId(int groupId, DateTime startDateTime, DateTime endDateTime);
    }
}
