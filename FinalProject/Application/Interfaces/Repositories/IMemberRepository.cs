using Application.Models.Member;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMemberRepository : IBaseRepository<Member, CreateMemberDto, UpdateMemberDto>
    {
    }
}
