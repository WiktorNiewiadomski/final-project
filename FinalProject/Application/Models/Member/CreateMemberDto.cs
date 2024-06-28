using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Member
{
    public class CreateMemberDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(MemberType))]
        public MemberType Type { get; set; }
        public int? TrainingGroupId { get; set; }
    }
}
