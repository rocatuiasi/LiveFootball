using CommunityToolkit.Mvvm.ComponentModel;

using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public partial class FixturesViewModel : ObservableObject
{
    [ObservableProperty] 
    private List<FixtureMatchModel> _matchesCollection = new();

    [ObservableProperty] 
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = string.Empty;
}