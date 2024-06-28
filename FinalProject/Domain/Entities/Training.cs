﻿namespace Domain.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public string? PreNotes { get; set; }
        public string? PostNotes { get; set; }
        public Group Group { get; set; }
        public DateTime TrainingStart { get; set; }
        public DateTime TrainingEnd { get; set; }
    }
}
