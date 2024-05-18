using System.Windows.Media.Imaging;

namespace LiveFootball.Core.Models;

public class LeagueStandingTeamModel
{
    #region Backing Fields and Properties

    public int Position { get; init; }

    public BitmapSource Logo { get; init; }

    public string Club { get; init; }

    public int MatchesPlayed { get; init; }

    public int MatchesWon { get; init; }

    public int MatchesDrawn { get; init; }

    public int MatchesLost { get; init; }

    public int GoalsFor { get; init; }

    public int GoalsAgainst { get; init; }

    public int GoalDifference { get; init; }

    public int Points { get; init; }

    public string Form { get; init; }

    #endregion
}