namespace FootballPlayerAPI.Entities
{
    public class FootballPlayer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Number { get; set; }
        public string? CurrentTeam { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;
    }
}
