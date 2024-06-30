using Application.Models.Group;
using Domain.Entities;

namespace Application.Mappers
{
	public class GroupMapper
	{
        public static GroupDto FromEntity(Group entity)
        {
            return new GroupDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                Coach = MemberMapper.FromEntity(entity.Coach)
            };
        }
    }
}

