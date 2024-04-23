using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public class MenuViewModel : ObservableObject
{
    #region Properties

    public ObservableCollection<MenuItemViewModel> Leagues { get; set; }

    public ObservableCollection<MenuItemViewModel> Competitions { get; set; }

    #endregion

    #region Constructors

    public MenuViewModel()
    {
        Leagues = new ObservableCollection<MenuItemViewModel>
        {
            new("Premier League"),
            new("La Liga"),
            new("Bundesliga"),
            new("Ligue 1"),
            new("Serie A"),
            new("Liga 1 - Superliga")
        };

        Competitions = new ObservableCollection<MenuItemViewModel>
        {
            new("Champions League"),
            new("Europa League"),
            new("Conference League"),
            new("Euro")
        };
    }

    #endregion
}