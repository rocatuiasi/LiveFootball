/**************************************************************************
 *                                                                        *
 *  File:        MenuItemModel.cs                                         *
 *  Description: LiveFootball.Core.Models Library                         *
 *               Represents a menu item, typically used for displaying    *
 *               league information in a UI.                              *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
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

