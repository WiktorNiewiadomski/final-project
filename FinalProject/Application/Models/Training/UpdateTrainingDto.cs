using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Training
{
    public class UpdateTrainingDto
    {
        public int? GroupId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? TrainingStart { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? TrainingEnd { get; set; }
        [DataType(DataType.Text)]
        public string? PreNotes { get; set; }
        [DataType(DataType.Text)]
        public string? PostNotes { get; set; }
    }
}
