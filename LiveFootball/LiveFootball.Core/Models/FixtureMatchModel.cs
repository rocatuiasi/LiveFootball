namespace LiveFootball.Core.Models;

public class FixtureMatchModel
{
    public string Date { get; set; }
    public TeamModel HomeTeam { get; set; }
    public TeamModel AwayTeam { get; set; }
}