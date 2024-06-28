using Application.Models.Training;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ITrainingService : IBaseService<Training, CreateTrainingDto, UpdateTrainingDto>
    {
    }
}
