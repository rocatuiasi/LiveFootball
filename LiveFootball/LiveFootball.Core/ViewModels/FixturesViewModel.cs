using CommunityToolkit.Mvvm.ComponentModel;

using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public partial class FixturesViewModel : ObservableObject
{
    [ObservableProperty]
    private List<MatchModel> _matchesCollection = new();

    [ObservableProperty] 
    private bool _isLoading;
}