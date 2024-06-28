using Application.Models.Member;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IMemberRepository : IBaseRepository<Member, CreateMemberDto, UpdateGroupDto>
    {
    }
}
