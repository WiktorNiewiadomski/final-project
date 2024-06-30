using Application.Attributes.TypeAuthorize;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.Models.Group;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
	{
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        [TypeAuthorize(new[] { MemberType.Owner, MemberType.Coach })]
        public GroupDto CreateGroup(
            [FromBody] CreateGroupDto dto
            )
        {
            
            return GroupMapper.FromEntity(_groupService.Create(dto));
        }

        [HttpPatch]
        [Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner, MemberType.Coach })]
        public GroupDto UpdateGroup(
            int id,
            [FromBody] UpdateGroupDto dto
            )
        {
            return GroupMapper.FromEntity(_groupService.Update(id, dto));
        }

        [HttpDelete]
        [Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner, MemberType.Coach })]
        public ActionResult DeleteGroup(int id)
        {
            _groupService.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        public List<GroupDto> GetAllGroups()
        {
            return _groupService.GetAll().Select(GroupMapper.FromEntity).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public GroupDto GetGroup(int id)
        {
            return GroupMapper.FromEntity(_groupService.GetById(id));
        }
    }
}

