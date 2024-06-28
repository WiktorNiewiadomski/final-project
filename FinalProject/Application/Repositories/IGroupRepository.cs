using Application.Models.Group;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group, CreateGroupDto, UpdateGroupDto>
    {
    }
}
