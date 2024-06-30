using Application.Interfaces.Services;
using Application.Mappers;
using Application.Models.Member;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
	{
		private IMemberService _memberService;

		public MemberController(IMemberService memberService)
		{
			_memberService = memberService;
		}

		[HttpPost]
		public MemberDto CreateMember(
			[FromBody] CreateMemberDto dto
			)
		{
			return MemberMapper.FromEntity(_memberService.Create(dto));
		}

		[HttpPatch]
		[Route("{id}")]
		public MemberDto UpdateMember(
			int id,
			[FromBody] UpdateMemberDto dto
			)
		{
			return MemberMapper.FromEntity(_memberService.Update(id, dto));
		}

		[HttpDelete]
		[Route("{id}")]
		public ActionResult DeleteMember(int id)
		{
			_memberService.DeleteById(id);
			return Ok();
		}

		[HttpGet]
		public List<MemberDto> GetAllMembers()
		{
			return _memberService.GetAll().Select(MemberMapper.FromEntity).ToList();
		}

		[HttpGet]
		[Route("{id}")]
		public MemberDto GetMember(int id)
		{
			return MemberMapper.FromEntity(_memberService.GetById(id));
		}
	}
}

