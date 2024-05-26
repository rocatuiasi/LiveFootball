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

public class MenuItemModel
{
    public BitmapSource Logo { get; set; }
    public string Name { get; }
    public string LeagueId { get; set; }
    
    public MenuItemModel(string leagueName, string leagueId, BitmapImage leagueLogo)
    {
        Name = leagueName;
        LeagueId = leagueId;
        Logo = leagueLogo;
    }
}