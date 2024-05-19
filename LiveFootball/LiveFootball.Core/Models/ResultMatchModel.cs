namespace LiveFootball.Core.Models;

public class ResultMatchModel
{
    public string Date { get; set; }
    public ExtendedTeamModel HomeTeam { get; set; }
    public ExtendedTeamModel AwayTeam { get; set; }
}