using System.Collections.ObjectModel;
using LiveFootballWpf.Models;

namespace LiveFootballWpf.ViewModels;

public class LeagueStandingViewModel : ViewModelBase
{
    #region Properties

    public ObservableCollection<LeagueStandingTeamModel> StandingTeams { get; set; }

    #endregion

    #region Constructors

    public LeagueStandingViewModel()
    {
        StandingTeams = new ObservableCollection<LeagueStandingTeamModel>
        {
            new(
                1, "Arsenal", 28, 20, 4, 4, 70, 24, 46, 64, "WWWWWW"),
            new(
                2, "Liverpool", 28, 19, 7, 2, 65, 26, 39, 64, "WWWWWD"),
            new(
                3, "Manchester City", 28, 19, 6, 3, 63, 28, 35, 63, "DWWWWD")
        };
    }

    #endregion
}