using System;
using Application.Models.Group;

namespace Application.Models.Training
{
	public class TrainingDto
	{
        public int Id { get; set; }
        public string? PreNotes { get; set; }
        public string? PostNotes { get; set; }
        public GroupDto Group { get; set; }
        public DateTime TrainingStart { get; set; }
        public DateTime TrainingEnd { get; set; }
    }
}

