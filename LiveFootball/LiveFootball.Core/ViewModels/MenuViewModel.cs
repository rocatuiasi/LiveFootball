using System.Net.Http;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using LiveFootball.Core.Helpers;
using LiveFootball.Core.Services;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    #region Properties

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    private CancellationTokenSource _fetchLiveGamesDataCancellationTokenSource = null!;

    [ObservableProperty] 
    private List<MenuItemViewModel> _leagues = null!;
    
    [ObservableProperty] 
    private bool _isLoading;

    #endregion

    #region Constructors

    public MenuViewModel(IFootballApiService? footballApiService = null,
                         IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService = deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>();

        LoadLeagues();
    }

    #endregion

    #region ICommand

    public ICommand AllGamesCommand => new AsyncRelayCommand(AllGamesFetchData);

    #endregion

    #region ICommand Execution

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

    private async void LoadLeagues()
    {
        try
        {
            // Fetch Leagues data
            var leaguesData = _footballService.GetLeaguesData();
            // Deserialize Leagues data
            var jsonData = JObject.Parse(leaguesData);
            Leagues = await _deserializeDataService.DeserializeLeaguesData(jsonData);
        }
        /*TODO: Update needed after merge
        using LiveFootball.Core.Exceptions;
        catch (DeserializationException)
        {
            StatusMessage = "No standing data available...";
        } 
        */
        catch (HttpRequestException)
        {
            // StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            // StatusMessage = "Oops, something went wrong";
        }
    }

    public async Task StartFetchingLiveGamesData()
    {
        _fetchLiveGamesDataCancellationTokenSource = new CancellationTokenSource();

        await FetchLiveGamesDataAsync(_fetchLiveGamesDataCancellationTokenSource.Token);
    }

    public void StopFetchingLiveGamesData()
    {
        _fetchLiveGamesDataCancellationTokenSource?.Cancel();
    }
}