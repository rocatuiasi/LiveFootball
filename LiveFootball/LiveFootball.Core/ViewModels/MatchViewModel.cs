using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public class MatchViewModel : ObservableObject
{
    public string Time { get; set; }
    public TeamViewModel HomeTeam { get; set; }
    public TeamViewModel AwayTeam { get; set; }
}