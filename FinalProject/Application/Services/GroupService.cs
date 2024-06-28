using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Group;
using Domain.Entities;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Group Create(CreateGroupDto dto)
        {
            return _groupRepository.Create(dto);
        }

        public void DeleteById(int id)
        {
            _groupRepository.DeleteById(id);
        }

        public Group GetById(int id)
        {
            return _groupRepository.GetById(id);
        }

        public List<Group> List()
        {
            return _groupRepository.List();
        }

        public Group Update(UpdateGroupDto dto)
        {
            return _groupRepository.Update(dto);
        }
    }
}
