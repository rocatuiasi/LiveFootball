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
    private List<MenuItemViewModel> _leagues;

    #endregion

    #region Constructors

    public MenuViewModel(IFootballApiService? footballApiService = null,
                         IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService =
            deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>(); 

        Leagues = new List<MenuItemViewModel>
        {
            new("Premier League", "39"),
            new("La Liga", "140"),
            new("Bundesliga", "78"),
            new("Ligue 1", "61"),
            new("Serie A", "135"),
            new("Liga 1 - Superliga", "283")
        };
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