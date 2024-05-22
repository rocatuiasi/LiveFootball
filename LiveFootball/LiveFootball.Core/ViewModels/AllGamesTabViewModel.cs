using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace LiveFootball.Core.ViewModels;

public partial class AllGamesTabViewModel : ObservableObject
{
    [ObservableProperty]
    private int _selectedTabIndex;

    public AllGamesTabViewModel()
    {
        PropertyChanged += SelectedIndex_Changed;
    }

    private async void SelectedIndex_Changed(object? sender, EventArgs e)
    {
        if (SelectedTabIndex != 0)
            Ioc.Default.GetRequiredService<MenuViewModel>().StopFetchingLiveGamesData();
        else
            await Ioc.Default.GetRequiredService<MenuViewModel>().StartFetchingLiveGamesData();
    }
}