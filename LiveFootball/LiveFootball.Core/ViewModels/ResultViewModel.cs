using CommunityToolkit.Mvvm.ComponentModel;
using LiveFootball.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveFootball.ViewModels
{
    public class ResultViewModel : ObservableObject
    {
        public ObservableCollection<LeagueExpanderViewModel> LeagueExpanderCollection { get; }

        public ResultViewModel()
        {
            LeagueExpanderCollection =
            [
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
                                RedCards = ["Roca", "Roca"],
                                YellowCards = ["Varvarik"],
                                Goals = "4"
                            },
                            AwayTeam = new TeamViewModel
                            {
                                Logo = null,
                                Name = "Manchester United",
                                RedCards = ["roca"],
                                YellowCards = ["Varvarik"],
                                Goals = "2"
                            }
                        },
                        new MatchViewModel
                        {
                            Time ="22:00",
                            HomeTeam = new TeamViewModel
                            {   Logo = null,
                                Name = "NewCastle",
                                RedCards = ["Roca"],
                                YellowCards = ["Varvarik"],
                                Goals = "0"
                            },
                            AwayTeam = new TeamViewModel
                            {
                                Logo = null,
                                Name = "Arsenal",
                                RedCards = ["Roca"],
                                YellowCards = ["Varvarik"],
                                Goals = "3"
                            }
                        }
                    ]

                },
                new LeagueExpanderViewModel
                {
                    Name = "Superliga",
                    MatchesCollection =
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
                                Goals = "2"
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
                                Goals = "1"
                            },
                            AwayTeam = new TeamViewModel
                            {
                                Logo = null,
                                Name = "Poli Iasi",
                                RedCards = ["Razvan"],
                                YellowCards = ["Varvarik"],
                                Goals = "1"
                            }
                        },

                        new MatchViewModel
                        {
                            Time = "20:00",
                            HomeTeam = new TeamViewModel
                            {
                                Logo = null,
                                Name = "Craiova",
                                RedCards = ["Razvan","Roca","Dragos"],
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

                    ]

                }

            ];
        }
    }
}
