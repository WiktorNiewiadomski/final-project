using Application.Models.Group;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group, CreateGroupDto, UpdateGroupDto>
    {
        List<Group> GetCoachGroups(int CoachId);
    }
}
