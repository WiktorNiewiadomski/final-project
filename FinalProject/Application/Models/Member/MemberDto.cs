using System;
using Application.Models.Group;
using Domain.Enums;

namespace Application.Models.Member
{
	public class MemberDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public MemberType Type { get; set; }
        public GroupDto? TrainingGroup { get; set; }

    }
}

