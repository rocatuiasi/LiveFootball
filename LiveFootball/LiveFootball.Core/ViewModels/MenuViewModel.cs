using System.Collections.ObjectModel;
using System.ComponentModel;
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

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

public partial class MenuViewModel : ObservableObject, IDisposable
{
    #region Properties and Fields

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    private CancellationTokenSource _fetchLiveGamesDataCancellationTokenSource;
    private CancellationTokenSource _filteringCancellationTokenSource;

    [ObservableProperty]
    private List<MenuItemViewModel> _leagues;
    
    [ObservableProperty]
    private ObservableCollection<MenuItemViewModel> _filteredLeagues;
    
    [ObservableProperty] 
    private bool _isLoading;

    [ObservableProperty]
    private string _statusMessage;

    [ObservableProperty]
    private string _searchText;

    #endregion

    #region Commands

    public ICommand AllGamesCommand => new AsyncRelayCommand(AllGamesFetchData);

    #endregion

    #region Constructors

    public MenuViewModel(IFootballApiService? footballApiService = null,
                         IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService = deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>();
        _fetchLiveGamesDataCancellationTokenSource = null!;
        _filteringCancellationTokenSource = new CancellationTokenSource();
        _leagues = new List<MenuItemViewModel>();
        _filteredLeagues = new ObservableCollection<MenuItemViewModel>();
        _statusMessage = string.Empty;
        _searchText = string.Empty;
       
        PropertyChanged += OnPropertyChanged;
        
        IsLoading = true;
        InitializeComponentAsync();
    }

    #endregion

    /// <summary>
    ///    Initializes the component asynchronously by fetching and deserializing the leagues data
    /// </summary>
    private async void InitializeComponentAsync()
    {
        try
        {
            // Fetch Leagues data
            var leaguesData = await _footballService.GetLeaguesDataAsync();
            // Deserialize Leagues data
            var jsonData = JObject.Parse(leaguesData);
            Leagues = await _deserializeDataService.DeserializeLeaguesData(jsonData);
        }
        catch (DeserializationException)
        {
            StatusMessage = "No standing data available...";
            Leagues = [];
        } 
        catch (HttpRequestException)
        {
            StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
            Leagues = [];
        }
        catch (Exception)
        {
            StatusMessage = "Oops, something went wrong";
            Leagues = [];
        }
    }

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
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(SearchText))
            OnSearchTextChanged();
        else if (e.PropertyName == nameof(Leagues))
            OnSearchTextChanged();
    }
    
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

    #region Commands Execution

    private async Task AllGamesFetchData()
    {
        // Switch current TabView to AllGamesTabView
        Ioc.Default.GetRequiredService<MainViewModel>().CurrentTabView = Ioc.Default.GetRequiredService<AllGamesTabViewModel>();

        await StartFetchingLiveGamesData();

        // TODO: Results and Fixtures
    }

    private async Task FetchLiveGamesDataAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                // Set loading state to true
                HelperFunctions.SetAllGamesLoadingProgressState(true);

                // Fetch Live Games data
                var liveGamesData = await _footballService.GetLiveGamesDataAsync();
                await RefreshLiveGames(JObject.Parse(liveGamesData));

                // Set loading state to false
                HelperFunctions.SetAllGamesLoadingProgressState(false);

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

    private async Task RefreshLiveGames(JObject jsonData)
    {
        var matchesCollection = await _deserializeDataService.DeserializeLiveGamesData(jsonData);

        var liveGamesViewModel = Ioc.Default.GetRequiredService<LiveGamesViewModel>();
        liveGamesViewModel.MatchesCollection = matchesCollection;
    }

    #endregion

    public async Task StartFetchingLiveGamesData()
    {
        _fetchLiveGamesDataCancellationTokenSource = new CancellationTokenSource();

        await FetchLiveGamesDataAsync(_fetchLiveGamesDataCancellationTokenSource.Token);
    }

    public void StopFetchingLiveGamesData()
    {
        _fetchLiveGamesDataCancellationTokenSource?.Cancel();
    }

    #region Filtering Operation Methods
    
    /// <summary>
    ///    Filters the leagues asynchronously
    /// </summary>
    /// <param name="searchText">Search text used to filter the leagues</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A task that represents the asynchronous filtering operation</returns>
    private async Task FilterAsync(string searchText, CancellationToken cancellationToken)
    {
        try
        {
            await Application.Current.Dispatcher.InvokeAsync(FilteredLeagues.Clear, DispatcherPriority.Background, cancellationToken);

            var filtered = string.IsNullOrWhiteSpace(searchText)
                ? Leagues
                : await Task.Run(() => FilterLeagues(searchText, Leagues, cancellationToken), cancellationToken);

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                FilteredLeagues = new ObservableCollection<MenuItemViewModel>(filtered);
            }, DispatcherPriority.Background, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // The operation was cancelled, no need to handle this exception
        }
    }

    /// <summary>
    ///    Filters the leagues
    /// </summary>
    /// <param name="searchText">Search text used to filter the leagues</param>
    /// <param name="leagues">The leagues to filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An enumerable of filtered leagues</returns>
    private IEnumerable<MenuItemViewModel> FilterLeagues(string searchText, IEnumerable<MenuItemViewModel> leagues, CancellationToken cancellationToken)
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
    ///   Determines whether the league name contains all the words
    /// </summary>
    /// <param name="league">The league to check</param>
    /// <param name="words">The words to check</param>
    /// <returns>True if the league name contains all the words, otherwise false</returns>
    private bool ContainsWords(MenuItemViewModel league, string[] words)
    {
        if (words.Length == 0)
            return true;

        var lowercaseName = (league.Name?.ToLower() ?? string.Empty).Replace(" ", "");
        int startIndex = 0;

        foreach (var word in words)
        {
            int index = lowercaseName.IndexOf(word, startIndex, StringComparison.CurrentCultureIgnoreCase);
            if (index == -1)
                return false;

            startIndex = index + word.Length;
        }

        return true;
    }

    #endregion
}