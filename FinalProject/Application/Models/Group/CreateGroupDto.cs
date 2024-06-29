using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Group
{
    public class CreateGroupDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        [EnumDataType(typeof(GroupType))]
        public GroupType Type { get; set; }
        [Required]
        public int CoachId { get; set; }
    }
}
