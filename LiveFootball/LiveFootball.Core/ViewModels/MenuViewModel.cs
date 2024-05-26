/**************************************************************************
 *                                                                        * 
 *  File:        MenuViewModel.cs                                         *
 *  Description: LiveFootball.Core.ViewModels Library                     *
 *               View model for managing the menu items and fetching      *
 *               live games data in the application.                      *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using LiveFootball.Core.Exceptions;
using LiveFootball.Core.Helpers;
using LiveFootball.Core.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

/// <summary>
/// View model for managing the menu items and fetching live games data in the application.
/// </summary>
public partial class MenuViewModel : ObservableObject, IDisposable
{
    #region Properties and Fields

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    private CancellationTokenSource _fetchLiveGamesDataCancellationTokenSource;
    private CancellationTokenSource _filteringCancellationTokenSource;

    /// <summary>
    /// The list of leagues.
    /// </summary>
    [ObservableProperty]
    private List<MenuItemViewModel> _leagues;

    /// <summary>
    /// The filtered list of leagues based on the search text.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<MenuItemViewModel> _filteredLeagues;

    /// <summary>
    /// The collection of favourite leagues.
    /// </summary>
    public ObservableCollection<MenuItemViewModel> FavouriteLeagues { get; }

    /// <summary>
    /// Indicates whether data is loading.
    /// </summary>
    [ObservableProperty]
    private bool _isLoading;

    /// <summary>
    /// The status message.
    /// </summary>
    [ObservableProperty]
    private string _statusMessage;

    /// <summary>
    /// The search text.
    /// </summary>
    [ObservableProperty]
    private string _searchText;

    #endregion

    #region Commands

    /// <summary>
    /// Command to fetch all games.
    /// </summary>
    public ICommand AllGamesCommand => new AsyncRelayCommand(AllGamesFetchData);

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
    /// </summary>
    public MenuViewModel(IFootballApiService? footballApiService = null,
                         IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService = deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>();
        _fetchLiveGamesDataCancellationTokenSource = new CancellationTokenSource();
        _filteringCancellationTokenSource = new CancellationTokenSource();
        _leagues = new List<MenuItemViewModel>();
        _filteredLeagues = new ObservableCollection<MenuItemViewModel>();
        FavouriteLeagues = new ObservableCollection<MenuItemViewModel>();
        _statusMessage = string.Empty;
        _searchText = string.Empty;

        PropertyChanged += OnPropertyChanged;
        FavouriteLeagues.CollectionChanged += FavouriteLeaguesOnCollectionChanged;

        InitializeComponentAsync();
    }

    #endregion

    /// <summary>
    /// Initializes the component asynchronously by fetching and deserializing the leagues data.
    /// </summary>
    private async void InitializeComponentAsync()
    {
        IsLoading = true;

        try
        {
            // Fetch Leagues data
            var leaguesData = await _footballService.GetLeaguesDataAsync();

            // Deserialize Leagues data
            var jsonData = JObject.Parse(leaguesData);
            var leaguesModel = await _deserializeDataService.DeserializeLeaguesData(jsonData);

            // Create MenuItemViewModel instances from the deserialized data
            var leagues = new List<MenuItemViewModel>();
            foreach (var model in leaguesModel)
                leagues.Add(new MenuItemViewModel(model));
            // Add the leagues to the Leagues collection
            Leagues = leagues;

            // Read the favourite leagues data
            await ReadFavouritesLeaguesDataAsync();
        }
        catch (DeserializationException)
        {
            Leagues = [];
            StatusMessage = "No standing data available...";
        }
        catch (HttpRequestException)
        {
            Leagues = [];
            StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            Leagues = [];
            StatusMessage = "Oops, something went wrong";
        }
    }

    #region IDisposable Method Implementation

    /// <summary>
    /// Disposes the object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _filteringCancellationTokenSource?.Dispose();
            _fetchLiveGamesDataCancellationTokenSource?.Dispose();
        }
    }

    #endregion

    #region Event Handlers

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SearchText))
            OnSearchTextChanged();
        else if (e.PropertyName == nameof(Leagues))
            OnSearchTextChanged();
    }

    /// <summary>
    ///     Filters the leagues when the search text changes
    /// </summary>
    private async void OnSearchTextChanged()
    {
        // Cancel any pending filtering operation
        await _filteringCancellationTokenSource.CancelAsync();

        // Create a new cancellation token source
        _filteringCancellationTokenSource = new CancellationTokenSource();

        IsLoading = true;
        // Perform filtering asynchronously
        await FilterAsync(SearchText.ToLower(), _filteringCancellationTokenSource.Token);
        IsLoading = false;
    }

    /// <summary>
    ///     Saves the favourite leagues data when the FavouriteLeagues collection changes
    /// </summary>
    private async void FavouriteLeaguesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Convert the FavouriteLeagues collection to a new collection with only Name and LeagueId properties
        var favouriteLeaguesData = FavouriteLeagues.Select(league => new { league.Name, league.LeagueId });
        await SaveFavouritesLeaguesData(favouriteLeaguesData);
    }

    #endregion

    #region Commands Execution

    private async Task AllGamesFetchData()
    {
        Ioc.Default.GetRequiredService<MainViewModel>().Title = "Today's football matches";

        // Switch current TabView to AllGamesTabView
        Ioc.Default.GetRequiredService<MainViewModel>().CurrentTabView =
            Ioc.Default.GetRequiredService<AllGamesTabViewModel>();

        await StartFetchingLiveGamesData();

        // Set loading state to true
        HelperFunctions.SetAllGamesLoadingProgressState(true);

        // Fetch Results data
        await RefreshAllGamesResults();

        // Fetch Fixtures data
        await RefreshAllGamesFixtures();

        // Set loading state to false
        HelperFunctions.SetAllGamesLoadingProgressState(false);
    }

    private async Task FetchLiveGamesDataAsync()
    {
        _fetchLiveGamesDataCancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _fetchLiveGamesDataCancellationTokenSource.Token;

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                // Set loading state to true
                HelperFunctions.SetLiveGamesLoadingProgressState(true);

                // Fetch Live Games data
                await RefreshLiveGames();

                // Set loading state to false
                HelperFunctions.SetLiveGamesLoadingProgressState(false);

                // Wait for the next interval
                await Task.Delay(60000, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // The operation was canceled, exit the method
                return;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    private async Task RefreshLiveGames()
    {
        var liveGamesViewModel = Ioc.Default.GetRequiredService<LiveGamesViewModel>();

        try
        {
            var liveGamesData = await _footballService.GetLiveGamesDataAsync();
            var jsonData = JObject.Parse(liveGamesData);
            // Deserialize Results data
            var matchesCollection = await _deserializeDataService.DeserializeLiveGamesData(jsonData);

            liveGamesViewModel.MatchesCollection = matchesCollection;
            liveGamesViewModel.StatusMessage = string.Empty;
        }
        catch (DeserializationException)
        {
            liveGamesViewModel.MatchesCollection =  [];
            liveGamesViewModel.StatusMessage = "No results data available...";
        }
        catch (HttpRequestException)
        {
            liveGamesViewModel.MatchesCollection =  [];
            liveGamesViewModel.StatusMessage =
                "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            liveGamesViewModel.MatchesCollection =  [];
            liveGamesViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    private async Task RefreshAllGamesResults()
    {
        var resultsViewModel = Ioc.Default.GetRequiredService<ResultsViewModel>();
        try
        {
            // Fetch Results data
            var resultsData = await _footballService.GetAllGamesResultsDataAsync();
            var jsonData = JObject.Parse(resultsData);
            // Deserialize Results data
            var matchesCollection = await _deserializeDataService.DeserializeResultsData(jsonData);

            resultsViewModel.MatchesCollection = matchesCollection;
            resultsViewModel.StatusMessage = string.Empty;
        }
        catch (DeserializationException)
        {
            resultsViewModel.MatchesCollection =  [];
            resultsViewModel.StatusMessage = "No results data available...";
        }
        catch (HttpRequestException)
        {
            resultsViewModel.MatchesCollection =  [];
            resultsViewModel.StatusMessage =
                "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            resultsViewModel.MatchesCollection =  [];
            resultsViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    private async Task RefreshAllGamesFixtures()
    {
        var fixturesViewModel = Ioc.Default.GetRequiredService<FixturesViewModel>();

        try
        {
            // Fetch Fixtures data
            var fixturesData = await _footballService.GetAllGamesFixturesDataAsync();
            var jsonData = JObject.Parse(fixturesData);
            // Deserialize Fixtures data
            var matchesCollection = await _deserializeDataService.DeserializeFixturesData(jsonData);

            fixturesViewModel.MatchesCollection = matchesCollection;
            fixturesViewModel.StatusMessage = string.Empty;
        }
        catch (DeserializationException)
        {
            fixturesViewModel.MatchesCollection =  [];
            fixturesViewModel.StatusMessage = "No more fixtures this season...";
        }
        catch (HttpRequestException)
        {
            fixturesViewModel.MatchesCollection =  [];
            fixturesViewModel.StatusMessage =
                "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            fixturesViewModel.MatchesCollection =  [];
            fixturesViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    #endregion

    public async Task StartFetchingLiveGamesData()
    {
        if (Ioc.Default.GetRequiredService<AllGamesTabViewModel>().SelectedTabIndex == 0)
        {
            if (!_fetchLiveGamesDataCancellationTokenSource.Token.IsCancellationRequested)
            {
                StopFetchingLiveGamesData();
            }

            await FetchLiveGamesDataAsync();
        }
    }

    public void StopFetchingLiveGamesData()
    {
        _fetchLiveGamesDataCancellationTokenSource?.Cancel();
    }

    #region Favourite Leagues Reading/Writing

    /// <summary>
    ///     Saves the favourite leagues data asynchronously
    /// </summary>
    /// <param name="favouriteLeaguesData">The favourite leagues data to save</param>
    /// <returns>A task that represents the asynchronous save operation</returns>
    private async Task SaveFavouritesLeaguesData(object favouriteLeaguesData)
    {
        // Convert the new collection to JSON
        var json = JsonConvert.SerializeObject(favouriteLeaguesData);

        // Write the JSON to the file asynchronously
        await File.WriteAllTextAsync("favourite-leagues.json", json);
    }

    /// <summary>
    ///     Reads the favourite leagues data asynchronously
    /// </summary>
    /// <returns>A task that represents the asynchronous read operation</returns>
    private async Task ReadFavouritesLeaguesDataAsync()
    {
        try
        {
            // Read the JSON from the file asynchronously
            var json = await File.ReadAllTextAsync("favourite-leagues.json");

            // Deserialize the JSON to the new collection
            var favouriteLeaguesData = JsonConvert.DeserializeObject<List<MenuItemViewModel>>(json);

            if (favouriteLeaguesData != null)
            {
                // Clear the existing favourite leagues collection
                FavouriteLeagues.Clear();

                // Add the deserialized favourite leagues to the collection
                foreach (var leagueData in favouriteLeaguesData)
                {
                    var item = Leagues.FirstOrDefault(x => x.LeagueId == leagueData.LeagueId);
                    if (item != null)
                        FavouriteLeagues.Add(item);
                }
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    #endregion

    #region Filtering Operation Methods

    /// <summary>
    ///     Filters the leagues asynchronously
    /// </summary>
    /// <param name="searchText">Search text used to filter the leagues</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task that represents the asynchronous filtering operation</returns>
    private async Task FilterAsync(string searchText, CancellationToken cancellationToken)
    {
        try
        {
            await Application.Current.Dispatcher.InvokeAsync(FilteredLeagues.Clear, DispatcherPriority.Background,
                cancellationToken);

            var filtered = string.IsNullOrWhiteSpace(searchText)
                               ? Leagues
                               : await Task.Run(() => FilterLeagues(searchText, Leagues, cancellationToken),
                                     cancellationToken);

            await Application.Current.Dispatcher.InvokeAsync(
                () => { FilteredLeagues = new ObservableCollection<MenuItemViewModel>(filtered); },
                DispatcherPriority.Background, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // The operation was cancelled, no need to handle this exception
        }
    }

    /// <summary>
    ///     Filters the leagues
    /// </summary>
    /// <param name="searchText">Search text used to filter the leagues</param>
    /// <param name="leagues">The leagues to filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An enumerable of filtered leagues</returns>
    private IEnumerable<MenuItemViewModel> FilterLeagues(string searchText, IEnumerable<MenuItemViewModel> leagues,
                                                         CancellationToken cancellationToken)
    {
        var words = searchText.Split(' ');
        var filtered = new List<MenuItemViewModel>();

        foreach (var league in leagues)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (ContainsWords(league, words))
                filtered.Add(league);
        }

        return filtered;
    }

    /// <summary>
    ///     Determines whether the league name contains all the words
    /// </summary>
    /// <param name="league">The league to check</param>
    /// <param name="words">The words to check</param>
    /// <returns>True if the league name contains all the words, otherwise false</returns>
    private bool ContainsWords(MenuItemViewModel league, string[] words)
    {
        if (words.Length == 0)
            return true;

        var lowercaseName = (league.Name?.ToLower() ?? string.Empty).Replace(" ", "");
        var startIndex = 0;

        foreach (var word in words)
        {
            var index = lowercaseName.IndexOf(word, startIndex, StringComparison.CurrentCultureIgnoreCase);

            if (index == -1)
                return false;

            startIndex = index + word.Length;
        }

        return true;
    }

    #endregion
}