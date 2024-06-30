using Application.Models.Training;
using Domain.Entities;

namespace Application.Mappers
{
	public class TrainingMapper
	{
        public static TrainingDto FromEntity(Training entity)
        {
            return new TrainingDto()
            {
                Id = entity.Id,
                PreNotes = entity.PreNotes,
                PostNotes = entity.PostNotes,
                Group = GroupMapper.FromEntity(entity.Group),
                TrainingStart = entity.TrainingStart,
                TrainingEnd = entity.TrainingEnd,
            };
        }
    }
}

