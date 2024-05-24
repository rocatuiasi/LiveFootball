using CommunityToolkit.Mvvm.ComponentModel;

using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    [ObservableProperty] 
    private List<ResultMatchModel> _matchesCollection = new();

    [ObservableProperty] 
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = string.Empty;
}