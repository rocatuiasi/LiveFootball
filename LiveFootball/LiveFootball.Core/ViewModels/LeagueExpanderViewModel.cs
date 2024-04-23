using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public class LeagueExpanderViewModel : ObservableObject
{
    //TODO: Remove member initializing when view is completed
    public string Name { get; set; } = "Superliga";

    public ObservableCollection<MatchViewModel> MatchesCollection { get; init; } =
    [
        new MatchViewModel
        {
            Time = "16:30",
            HomeTeam = new TeamViewModel
            {
                Logo = null,
                Name = "Steaua",
                RedCards = ["Razvan", "Hordila"],
                YellowCards = [],
                Goals = "1"
            },
            AwayTeam = new TeamViewModel
            {
                Logo = null,
                Name = "Dinamo",
                RedCards = [],
                YellowCards = ["Varvarik"],
                Goals = "0"
            }
        },

        new MatchViewModel
        {
            Time = "17:00",
            HomeTeam = new TeamViewModel
            {
                Logo = null,
                Name = "Botosani",
                RedCards = [],
                YellowCards = ["Varvarik"],
                Goals = "0"
            },
            AwayTeam = new TeamViewModel
            {
                Logo = null,
                Name = "Poli Iasi",
                RedCards = ["Razvan"],
                YellowCards = ["Varvarik"],
                Goals = "3"
            }
        },

        new MatchViewModel
        {
            Time = "20:00",
            HomeTeam = new TeamViewModel
            {
                Logo = null,
                Name = "Craiova",
                RedCards = ["Razvan"],
                YellowCards = ["Varvarik"],
                Goals = "2"
            },
            AwayTeam = new TeamViewModel
            {
                Logo = null,
                Name = "Rapid",
                RedCards = ["Razvan"],
                YellowCards = ["Varvarik"],
                Goals = "2"
            }
        }
    ];
}