/**************************************************************************
 *                                                                        * 
 *  File:        LeagueExpanderViewModel.cs                               *
 *  Description: LiveFootball.Core.ViewModels Library                     *
 *               View model for managing league expander data.            *
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


// TODO : For the future expansions of All Games tabs
public partial class LeagueExpanderViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty] 
    private List<FixtureMatchModel> _matchesCollection;
}