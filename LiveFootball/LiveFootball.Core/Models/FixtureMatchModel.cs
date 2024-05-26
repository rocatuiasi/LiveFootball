namespace LiveFootball.Core.Models;

/// <summary>
/// Represents a fixture match model containing information about a scheduled match.
/// </summary>
public class FixtureMatchModel
{
    /// <summary>
    /// Gets or sets the date and time of the match.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Gets or sets the home team participating in the match.
    /// </summary>
    public TeamModel HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the away team participating in the match.
    /// </summary>
    public TeamModel AwayTeam { get; set; }
}