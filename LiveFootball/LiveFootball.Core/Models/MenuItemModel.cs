using System.Windows.Media.Imaging;

namespace LiveFootball.Core.Models;

/// <summary>
/// Represents a menu item model for displaying league information.
/// </summary>
public class MenuItemModel
{
    /// <summary>
    /// Gets or sets the logo of the league.
    /// </summary>
    public BitmapSource Logo { get; set; }

    /// <summary>
    /// Gets the name of the league.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets the ID of the league.
    /// </summary>
    public string LeagueId { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuItemModel"/> class with league information.
    /// </summary>
    /// <param name="leagueName">The name of the league.</param>
    /// <param name="leagueId">The ID of the league.</param>
    /// <param name="leagueLogo">The logo of the league.</param>
    public MenuItemModel(string leagueName, string leagueId, BitmapImage leagueLogo)
    {
        Name = leagueName;
        LeagueId = leagueId;
        Logo = leagueLogo;
    }
}

