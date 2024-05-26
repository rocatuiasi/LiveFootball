/**************************************************************************
 *                                                                        * 
 *  File:        MenuItemViewModel.cs                                     *
 *  Description: LiveFootball.Core.ViewModels Library                     *
 *              View model for managing the menu items in the application.*
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System.Net.Http;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using LiveFootball.Core.Exceptions;
using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;
using LiveFootball.Core.Services;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

/// <summary>
/// View model for managing the menu items in the application.
/// </summary>
public class MenuItemViewModel
{
    #region Backing Fields and Properties

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    /// <summary>
    /// The logo of the menu item.
    /// </summary>
    public BitmapSource Logo { get; set; }

    /// <summary>
    /// The name of the menu item.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The ID of the league associated with the menu item.
    /// </summary>
    public string LeagueId { get; set; }

    #endregion

    #region Commands

    /// <summary>
    /// Command to fetch data associated with the menu item.
    /// </summary>
    public ICommand FetchDataCommand => new AsyncRelayCommand(FetchData);

    /// <summary>
    /// Command to add the league associated with the menu item to favorites.
    /// </summary>
    public ICommand AddFavouriteCommand => new RelayCommand<string>(AddFavourite);

    /// <summary>
    /// Command to remove the league associated with the menu item from favorites.
    /// </summary>
    public ICommand RemoveFavouriteCommand => new RelayCommand<string>(RemoveFavourite);

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
    /// </summary>
    public MenuItemViewModel(string name, string leagueId, BitmapSource logo,
                             IFootballApiService? footballApiService = null,
                             IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService = deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>();

        Name = name;
        LeagueId = leagueId;
        Logo = logo;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
    /// </summary>
    public MenuItemViewModel(MenuItemModel menuItemModel) : this(menuItemModel.Name, menuItemModel.LeagueId,
        menuItemModel.Logo)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
    /// </summary>
    public MenuItemViewModel() : this(null, null, null) { }

    #endregion

    #region Commands Execution

    private async Task FetchData()
    {
        Ioc.Default.GetRequiredService<MenuViewModel>().StopFetchingLiveGamesData();

        // Switch current TabView to LeagueTabView
        Ioc.Default.GetRequiredService<MainViewModel>().Title = $"{Name} - Football matches";
        Ioc.Default.GetRequiredService<MainViewModel>().CurrentTabView =
            Ioc.Default.GetRequiredService<LeagueTabViewModel>();

        // Set loading state to true
        HelperFunctions.SetLeagueLoadingProgressState(true);

        // Fetch and Deserialize data
        await RefreshResults();
        await RefreshFixtures();
        await RefreshLeagueStanding();

        // Set loading state to false
        HelperFunctions.SetLeagueLoadingProgressState(false);
    }

    private void AddFavourite(string? leagueId)
    {
        if (leagueId == null) return;

        var menuViewModel = Ioc.Default.GetRequiredService<MenuViewModel>();
        if (menuViewModel.FavouriteLeagues.All(x => x.LeagueId != leagueId))
        {
            var leagueToAdd = menuViewModel.Leagues.First(x => x.LeagueId == leagueId);
            menuViewModel.FavouriteLeagues.Add(leagueToAdd);
        }
    }

    private void RemoveFavourite(string? leagueId)
    {
        if (leagueId == null) return;

        var menuViewModel = Ioc.Default.GetRequiredService<MenuViewModel>();
        var leagueToRemove = menuViewModel.FavouriteLeagues.First(x => x.LeagueId == leagueId);
        menuViewModel.FavouriteLeagues.Remove(leagueToRemove);
    }

    #endregion

    private async Task RefreshLeagueStanding()
    {
        var leagueStandingViewModel = Ioc.Default.GetRequiredService<LeagueStandingViewModel>();
        try
        {
            // Fetch Standing data
            var standingData = await _footballService.GetLeagueStandingDataAsync(LeagueId);
            var jsonData = JObject.Parse(standingData);

            // Deserialize Standing data
            var leagueStandingCollection = await _deserializeDataService.DeserializeStandingData(jsonData);

            leagueStandingViewModel.StandingTeams = leagueStandingCollection;
            leagueStandingViewModel.StatusMessage = string.Empty;
        }
        catch (DeserializationException)
        {
            leagueStandingViewModel.StandingTeams = [];
            leagueStandingViewModel.StatusMessage = "No standing data available...";
        }
        catch (HttpRequestException)
        {
            leagueStandingViewModel.StandingTeams = [];
            leagueStandingViewModel.StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            leagueStandingViewModel.StandingTeams = [];
            leagueStandingViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    private async Task RefreshFixtures()
    {
        var fixturesViewModel = Ioc.Default.GetRequiredService<FixturesViewModel>();
        try
        {
            // Fetch Fixtures data
            var fixturesData = await _footballService.GetLeagueFixturesDataAsync(LeagueId);
            var jsonData = JObject.Parse(fixturesData);
            // Deserialize Fixtures data
            var matchesCollection = await _deserializeDataService.DeserializeFixturesData(jsonData);

            fixturesViewModel.MatchesCollection = matchesCollection;
            fixturesViewModel.StatusMessage = string.Empty;
        }
        catch (DeserializationException)
        {
            fixturesViewModel.MatchesCollection = [];
            fixturesViewModel.StatusMessage = "No more fixtures this season...";
        }
        catch (HttpRequestException)
        {
            fixturesViewModel.MatchesCollection = [];
            fixturesViewModel.StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            fixturesViewModel.MatchesCollection = [];
            fixturesViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    private async Task RefreshResults()
    {
        var resultsViewModel = Ioc.Default.GetRequiredService<ResultsViewModel>();
        try
        {
            // Fetch Results data
            var resultsData = await _footballService.GetLeagueResultsDataAsync(LeagueId);
            var jsonData = JObject.Parse(resultsData);
            // Deserialize Results data
            var matchesCollection = await _deserializeDataService.DeserializeResultsData(jsonData);

            resultsViewModel.MatchesCollection = matchesCollection;
            resultsViewModel.StatusMessage = string.Empty;
        }
        catch (DeserializationException)
        {
            resultsViewModel.MatchesCollection = [];
            resultsViewModel.StatusMessage = "No results data available...";
        }
        catch (HttpRequestException)
        {
            resultsViewModel.MatchesCollection = [];
            resultsViewModel.StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            resultsViewModel.MatchesCollection = [];
            resultsViewModel.StatusMessage = "Oops, something went wrong";
        }
    }
}