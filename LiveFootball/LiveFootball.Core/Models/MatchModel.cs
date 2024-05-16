namespace LiveFootball.Core.Models;

public class MatchModel
{
    public string Date { get; set; }
    public TeamModel HomeTeam { get; set; }
    public TeamModel AwayTeam { get; set; }
}