using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace LiveFootball.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] 
    private object _currentTabView;

    public MainViewModel()
    {
        CurrentTabView = Ioc.Default.GetRequiredService<LeagueTabViewModel>();
    }
}