using System.Windows.Media.Imaging;

namespace LiveFootball.Core.Models;

/// <summary>
/// Represents a team model containing basic information about a team.
/// </summary>
public class TeamModel
{
    /// <summary>
    /// Gets or sets the logo of the team.
    /// </summary>
    public BitmapSource Logo { get; set; }

    /// <summary>
    /// Gets or sets the name of the team.
    /// </summary>
    public string Name { get; set; }
}