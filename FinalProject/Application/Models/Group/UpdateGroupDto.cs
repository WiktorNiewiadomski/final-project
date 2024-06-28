using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Group
{
    public class UpdateGroupDto
    {
        [MaxLength(40)]
        [MinLength(4)]
        public string? Name { get; set; }
        [MinLength(8)]
        public string? Description { get; set; }
        [EnumDataType(typeof(GroupType))]
        public GroupType? Type { get; set; }
        public int? CoachId { get; set; }
    }
}
