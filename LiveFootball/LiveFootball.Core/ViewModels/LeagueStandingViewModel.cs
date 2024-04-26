using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public class LeagueStandingViewModel : ObservableObject
{
    #region Properties

    public ObservableCollection<LeagueStandingTeamModel> StandingTeams { get; set; }

    #endregion

    #region Constructors

    public LeagueStandingViewModel()
    {
        StandingTeams = new ObservableCollection<LeagueStandingTeamModel>();
    }

    #endregion
}