using CommunityToolkit.Mvvm.ComponentModel;
using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

/// <summary>
/// Represents the ViewModel for managing live games data and related functionality.
/// </summary>
/// <remarks>
/// This ViewModel likely contains properties and methods for displaying live games data
/// and handling user interactions related to live games.
/// </remarks>
/// <seealso cref="LiveFootball.Core.Models.LiveMatchModel"/>
public partial class LiveGamesViewModel : ObservableObject
{
    [ObservableProperty]
    private List<LiveMatchModel> _matchesCollection = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = string.Empty;
}