using Application.Attributes.TypeAuthorize;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.Models.Member;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
	[Authorize]
    public class MemberController : ControllerBase
	{
		private IMemberService _memberService;

		public MemberController(IMemberService memberService)
		{
			_memberService = memberService;
		}

		[HttpPost]
        [TypeAuthorize(new[] { MemberType.Owner })]
        public MemberDto CreateMember(
			[FromBody] CreateMemberDto dto
			)
		{
			return MemberMapper.FromEntity(_memberService.Create(dto));
		}

		[HttpPatch]
		[Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner })]
        public MemberDto UpdateMember(
			int id,
			[FromBody] UpdateMemberDto dto
			)
		{
			return MemberMapper.FromEntity(_memberService.Update(id, dto));
		}

		[HttpDelete]
		[Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner })]
        public ActionResult DeleteMember(int id)
		{
			_memberService.DeleteById(id);
			return Ok();
		}

		[HttpGet]
        [TypeAuthorize(new[] { MemberType.Owner })]
        public List<MemberDto> GetAllMembers()
		{
			return _memberService.GetAll().Select(MemberMapper.FromEntity).ToList();
		}

		[HttpGet]
		[Route("{id}")]
        [TypeAuthorize(new[] { MemberType.Owner })]
        public MemberDto GetMember(int id)
		{
			return MemberMapper.FromEntity(_memberService.GetById(id));
		}

		[HttpPost]
		[Route("login")]
		[AllowAnonymous]
        public string LoginMember([FromBody] LoginMemberDto dto)
		{
			return _memberService.Login(dto);
		}

		[HttpGet]
		[Route("me")]
		[Authorize]
        public MemberDto GetMeMember()
		{
			var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            return MemberMapper.FromEntity(_memberService.GetById(userId));
		}
	}
}

