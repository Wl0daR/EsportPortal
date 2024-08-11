using EsportPortal.Models;

public class TournamentDetailsDto
{
    public string TournamentName { get; set; }
    public string LogoUrl { get; set; }
    public List<TeamDto> Teams { get; set; }
}