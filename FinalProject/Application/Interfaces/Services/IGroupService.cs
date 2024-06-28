using Application.Models.Group;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IGroupService : IBaseService<Group, CreateGroupDto, UpdateGroupDto>
    {
    }
}
