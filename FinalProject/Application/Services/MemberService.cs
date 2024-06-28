using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Member;
using Domain.Entities;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Member Create(CreateMemberDto dto)
        {
            return _memberRepository.Create(dto);
        }

        public void DeleteById(int id)
        {
            _memberRepository.DeleteById(id);
        }

        public Member GetById(int id)
        {
            return _memberRepository.GetById(id);
        }

        public List<Member> List()
        {
            return _memberRepository.List();
        }

        public Member Update(UpdateMemberDto dto)
        {
            return _memberRepository.Update(dto);
        }
    }
}
