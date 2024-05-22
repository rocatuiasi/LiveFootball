namespace LiveFootball.Core.Models;

public class LiveMatchModel
{
    public string Status { get; set; }
    public ExtendedTeamModel HomeTeam { get; set; }
    public ExtendedTeamModel AwayTeam { get; set; }
}