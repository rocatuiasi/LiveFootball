namespace LiveFootball.Core.Models;

/// <summary>
/// Represents a result match model containing information about a completed match.
/// </summary>
public class ResultMatchModel
{
    /// <summary>
    /// Gets or sets the date and time of the match.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Gets or sets the home team participating in the match.
    /// </summary>
    public ExtendedTeamModel HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the away team participating in the match.
    /// </summary>
    public ExtendedTeamModel AwayTeam { get; set; }
}