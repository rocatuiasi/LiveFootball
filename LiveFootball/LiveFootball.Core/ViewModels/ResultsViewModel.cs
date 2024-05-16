using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels
{
    public class ResultsViewModel : ObservableObject
    {
        public ObservableCollection<LeagueExpanderViewModel> LeagueExpanderCollection { get; }

        public ResultsViewModel()
        {
            
        }
    }
}
