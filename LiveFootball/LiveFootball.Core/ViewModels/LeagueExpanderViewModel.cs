using CommunityToolkit.Mvvm.ComponentModel;
using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;


// TODO : For the future expansions of All Games tabs
public partial class LeagueExpanderViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty] 
    private List<MatchModel> _matchesCollection;
}