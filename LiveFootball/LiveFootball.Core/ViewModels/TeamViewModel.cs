using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LiveFootball.Core.ViewModels;

public class TeamViewModel : ObservableObject
{
    public Image Logo { get; set; }
    public string Name { get; set; }
    public ObservableCollection<string> RedCards { get; set; }
    public ObservableCollection<string> YellowCards { get; set; }
    public string Goals { get; set; }
}