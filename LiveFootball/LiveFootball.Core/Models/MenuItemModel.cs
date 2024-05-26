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