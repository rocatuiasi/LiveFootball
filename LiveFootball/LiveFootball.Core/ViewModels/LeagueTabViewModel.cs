using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public partial class LeagueTabViewModel : ObservableObject
{
    public string CurrentDate { get; } = DateTime.Now.ToString("MMMM dd, yyyy");
    
    [ObservableProperty]
    private string _title = "Football matches";
}