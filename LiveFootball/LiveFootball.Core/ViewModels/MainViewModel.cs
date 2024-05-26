using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace LiveFootball.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] 
    private object _currentTabView = null!;

    
    [ObservableProperty]
    private string _title = "Football matches";
    
    public string CurrentDate { get; } = DateTime.Now.ToString("MMMM dd, yyyy");
    
    public void InitializeApp()
    {
        Ioc.Default.GetRequiredService<MenuViewModel>().AllGamesCommand.Execute(null);
    }
}