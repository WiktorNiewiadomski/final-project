using Domain.Enums;

namespace Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GroupType Type { get; set; }
        public int CoachId { get; set; }
        public Member Coach { get; set; }
        public ISet<Training> GroupTrainings { get; set; }
        public ISet<Member> GroupPlayers { get; set; }
    }
}
