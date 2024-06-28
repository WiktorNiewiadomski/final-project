using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Training
{
    public class CreateTrainingDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TrainingStart { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TrainingEnd { get; set; }
        [DataType(DataType.Text)]
        public string? PreNotes { get; set; }
        [DataType(DataType.Text)]
        public string? PostNotes { get; set; }
        
    }
}
