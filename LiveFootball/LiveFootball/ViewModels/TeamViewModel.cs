using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LiveFootball.ViewModels;

public class TeamViewModel : ViewModelBase
{
    public Image Logo{ get; set; }
    public string Name{ get; set; }
    public ObservableCollection<string> RedCards{ get; set; }
    public ObservableCollection<string> YellowCards { get; set; }
    public string Goals { get; set; }
}