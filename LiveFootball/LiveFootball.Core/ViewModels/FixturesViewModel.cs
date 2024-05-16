using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public partial class FixturesViewModel : ObservableObject
{
    [ObservableProperty]
    private LeagueExpanderViewModel _league;

    [ObservableProperty] 
    private bool _isLoading;

    public FixturesViewModel()
    {
        League = new LeagueExpanderViewModel();
    }
}