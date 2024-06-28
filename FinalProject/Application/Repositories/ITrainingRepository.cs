using Application.Models.Training;
using Domain.Entities;

namespace Application.Repositories
{
    public interface ITrainingRepository : IBaseRepository<Training, CreateTrainingDto, UpdateTrainingDto>
    {
    }
}
