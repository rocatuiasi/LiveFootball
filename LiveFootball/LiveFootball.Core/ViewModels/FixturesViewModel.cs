/**************************************************************************
 *                                                                        * 
 *  File:        FixturesViewModel.cs                                     *
 *  Description: LiveFootball Core Library                                *
 *               ViewModel for managing fixtures data and related         *
 *               functionality.                                           *
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
/// ViewModel for managing fixtures data and related functionality.
/// </summary>
public partial class FixturesViewModel : ObservableObject
{
    [ObservableProperty] 
    private List<FixtureMatchModel> _matchesCollection = new();

    [ObservableProperty] 
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage = string.Empty;
}