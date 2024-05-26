namespace LiveFootball.Core.Models;

/// <summary>
/// Represents an extended team model including additional properties like goals.
/// </summary>
public class ExtendedTeamModel: TeamModel
{
    /// <summary>
    /// Gets or sets the number of goals scored by the team.
    /// </summary>
    public int Goals { get; set; }
}