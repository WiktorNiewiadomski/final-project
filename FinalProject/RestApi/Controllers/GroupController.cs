using Application.Interfaces.Services;
using Application.Mappers;
using Application.Models.Group;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
	{
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public GroupDto CreateMember(
            [FromBody] CreateGroupDto dto
            )
        {
            
            return GroupMapper.FromEntity(_groupService.Create(dto));
        }

        [HttpPatch]
        [Route("{id}")]
        public GroupDto UpdateMember(
            int id,
            [FromBody] UpdateGroupDto dto
            )
        {
            return GroupMapper.FromEntity(_groupService.Update(id, dto));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteMember(int id)
        {
            _groupService.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        public List<GroupDto> GetAllMembers()
        {
            return _groupService.GetAll().Select(GroupMapper.FromEntity).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public GroupDto GetMember(int id)
        {
            return GroupMapper.FromEntity(_groupService.GetById(id));
        }
    }
}

