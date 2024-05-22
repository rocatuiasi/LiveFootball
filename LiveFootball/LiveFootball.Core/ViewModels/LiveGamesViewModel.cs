using CommunityToolkit.Mvvm.ComponentModel;

using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public partial class LiveGamesViewModel : ObservableObject
{
    [ObservableProperty]
    private List<LiveMatchModel> _matchesCollection = new();

    [ObservableProperty]
    private bool _isLoading;
}