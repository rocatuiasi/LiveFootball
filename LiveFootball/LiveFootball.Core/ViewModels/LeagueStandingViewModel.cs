using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public partial class LeagueStandingViewModel : ObservableObject
{
    #region Properties

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private List<LeagueStandingTeamModel> _standingTeams = new();

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    #endregion
}