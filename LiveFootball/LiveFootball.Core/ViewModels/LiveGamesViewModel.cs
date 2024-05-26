/**************************************************************************
 *                                                                        * 
 *  File:        LiveGamesViewModel.cs                                    *
 *  Description: LiveFootball.Core.ViewModels Library                     *
 *               View model for managing live games.                      *
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
using LiveFootball.Core.Models;

namespace LiveFootball.Core.ViewModels;

/// <summary>
/// Represents the ViewModel for managing live games data and related functionality.
/// </summary>
/// <remarks>
/// This ViewModel likely contains properties and methods for displaying live games data
/// and handling user interactions related to live games.
/// </remarks>
/// <seealso cref="LiveFootball.Core.Models.LiveMatchModel"/>
public partial class LiveGamesViewModel : ObservableObject
{
    [ObservableProperty]
    private List<LiveMatchModel> _matchesCollection = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = string.Empty;
}