using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace LiveFootball.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] 
    private object _currentTabView = null!;

    public void InitializeApp()
    {
        Ioc.Default.GetRequiredService<MenuViewModel>().AllGamesCommand.Execute(null);
    }
}