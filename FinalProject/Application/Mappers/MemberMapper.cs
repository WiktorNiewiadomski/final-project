using Application.Models.Member;
using Domain.Entities;

namespace Application.Mappers
{
	public class MemberMapper
	{
		public static MemberDto FromEntity(Member entity)
		{
			return new MemberDto()
			{
				Id = entity.Id,
				Name = entity.Name,
				Type = entity.Type,
				TrainingGroup = entity.TrainingGroup != null ? GroupMapper.FromEntity(entity.TrainingGroup) : null
			};
		}
	}
}

