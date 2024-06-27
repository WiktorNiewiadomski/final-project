using Domain.Enums;

namespace Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public MemberType Type { get; set; }
        public Group? TrainingGroup { get; set; }
    }
}
