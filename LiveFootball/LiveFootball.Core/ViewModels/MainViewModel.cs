/**************************************************************************
 *                                                                        * 
 *  File:        MainViewModel.cs                                         *
 *  Description: LiveFootball.Core.ViewModels Library                      *
 *               View model for managing the main application view.        *
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

/// <summary>
/// Represents the main ViewModel responsible for managing the application's main view and related functionality.
/// </summary>
/// <remarks>
/// This ViewModel initializes the application and manages the current tab view and title displayed in the UI.
/// </remarks>
/// <seealso cref="LiveFootball.Core.ViewModels.ObservableObject"/>
public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] 
    private object _currentTabView = null!;

    [ObservableProperty]
    private string _title = "Football matches";

    /// <summary>
    /// Gets the current date formatted as "MMMM dd, yyyy".
    /// </summary>
    public string CurrentDate { get; } = DateTime.Now.ToString("MMMM dd, yyyy");

    /// <summary>
    /// Initializes the application by executing the AllGamesCommand of the MenuViewModel.
    /// </summary>
    public void InitializeApp()
    {
        Ioc.Default.GetRequiredService<MenuViewModel>().AllGamesCommand.Execute(null);
    }
}