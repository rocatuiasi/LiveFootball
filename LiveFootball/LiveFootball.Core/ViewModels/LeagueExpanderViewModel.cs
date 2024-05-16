using CommunityToolkit.Mvvm.ComponentModel;
using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

public partial class LeagueExpanderViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty] 
    private List<MatchModel> _matchesCollection;
}