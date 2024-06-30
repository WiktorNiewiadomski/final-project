using System;
using Application.Models.Member;
using Domain.Enums;

namespace Application.Models.Group
{
	public class GroupDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GroupType Type { get; set; }
        public MemberDto Coach { get; set; }
    }
}

