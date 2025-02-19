namespace FootballPlayerAPI.DTOs
{
    public class FootballPlayerDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Number { get; set; }
        public string? CurrentTeam { get; set; } = string.Empty;
    }
}
