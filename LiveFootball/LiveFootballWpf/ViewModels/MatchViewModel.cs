namespace LiveFootballWpf.ViewModels;

public class MatchViewModel : ViewModelBase
{
    public string Time { get; set; }
    public TeamViewModel HomeTeam { get; set; }
    public TeamViewModel AwayTeam { get; set; }
}