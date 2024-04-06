using LiveFootball.Models;

using System.Collections.ObjectModel;

namespace LiveFootball.ViewModels;

public class MenuViewModel : ViewModelBase
{
    #region Properties

    public ObservableCollection<MenuModel> Leagues { get; set; }

    public ObservableCollection<MenuModel> Competitions {  get; set; } 

    #endregion

    #region Constructors
    public MenuViewModel() 
    {
        Leagues = new ObservableCollection<MenuModel>()
        {
            new("Premier League"),
            new("La Liga"),
            new("Bundesliga"),
            new("Ligue 1"),
            new("Serie A"),
            new("Liga 1 - Superliga")
        };

        Competitions = new ObservableCollection<MenuModel>()
        {
            new("Champions League"),
            new("Europa League"),
            new("Conference League"),
            new("Euro")
        };
    }

    #endregion
}
