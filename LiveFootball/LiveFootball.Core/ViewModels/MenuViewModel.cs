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
            new("Premier League", "39"),
            new("La Liga", "140"),
            new("Bundesliga", "78"),
            new("Ligue 1", "61"),
            new("Serie A", "135"),
            new("Liga 1 - Superliga", "283")
        };

        Competitions = new ObservableCollection<MenuItemViewModel>
        {
            // TODO : Special case for this type of competitions 
            /*new("Champions League"),
            new("Europa League"),
            new("Conference League"),
            new("Euro")*/
        };
    }

    #endregion
}