namespace LiveFootball.Core.Models;

public class ExtendedMatchModel
{
    public string Date { get; set; }
    public ExtendedTeamModel HomeTeam { get; set; }
    public ExtendedTeamModel AwayTeam { get; set; }
}