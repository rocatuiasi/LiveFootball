namespace LiveFootball.Core.Models;

/// <summary>
/// Represents a live match with status and participating teams' information.
/// </summary>
public class LiveMatchModel
{
    /// <summary>
    /// Gets or sets the status of the match (e.g., live, half-time, full-time).
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the home team participating in the match.
    /// </summary>
    public ExtendedTeamModel HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the away team participating in the match.
    /// </summary>
    public ExtendedTeamModel AwayTeam { get; set; }
}