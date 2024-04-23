using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public class FixturesViewModel : ObservableObject
{
    public ObservableCollection<LeagueExpanderViewModel> LeagueExpanderCollection { get; }

    public FixturesViewModel()
    {
        LeagueExpanderCollection =
        [
            new LeagueExpanderViewModel(),
            new LeagueExpanderViewModel
            {
                Name = "Premier League",
                MatchesCollection =
                [
                    new MatchViewModel
                    {
                        Time = "18:30",
                        HomeTeam = new TeamViewModel
                        {
                            Logo = null,
                            Name = "Manchester City",
                            RedCards = ["Razvan", "Razvan"],
                            YellowCards = ["Varvarik"],
                            Goals = "4"
                        },
                        AwayTeam = new TeamViewModel
                        {
                            Logo = null,
                            Name = "Manchester United",
                            RedCards = ["Razvan"],
                            YellowCards = ["Varvarik"],
                            Goals = "2"
                        }
                    }
                ]
            }
        ];
    }
}