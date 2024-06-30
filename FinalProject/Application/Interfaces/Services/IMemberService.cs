using Application.Models.Member;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IMemberService : IBaseService<Member, CreateMemberDto, UpdateMemberDto>
    {
        string Login(LoginMemberDto dto);
    }
}
