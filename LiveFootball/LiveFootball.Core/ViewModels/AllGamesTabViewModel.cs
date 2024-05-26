/**************************************************************************
 *                                                                        * 
 *  File:        AllGamesTabViewModel.cs                                  *
 *  Description: LiveFootball.Core.ViewModels Library                      *
 *               View model for managing the All Games tab.                *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
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