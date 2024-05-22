using System.Windows.Input;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using LiveFootball.Core.Helpers;
using LiveFootball.Core.Services;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

public class MenuItemViewModel
{
    #region Backing Fields and Properties

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    public string Name { get; set; }
    private string LeagueId { get; }

    #endregion


    #region ICommand

    public ICommand FetchDataCommand => new AsyncRelayCommand(FetchData);

    #endregion


    #region Constructors

    public MenuItemViewModel(string name, string leagueId, IFootballApiService? footballApiService = null,
                             IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService = deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>();

        Name = name;
        LeagueId = leagueId;
    }

    #endregion


    #region ICommand Execution

    private async Task FetchData()
    {
        // Set loading state to true
        HelperFunctions.SetLeagueLoadingProgressState(true);

        // Fetch Results data
        var resultsData = await _footballService.GetResultsDataAsync(LeagueId);
        await RefreshResults(JObject.Parse(resultsData));

        // Fetch Fixtures data
        var fixturesData = await _footballService.GetFixturesDataAsync(LeagueId);
        await RefreshFixtures(JObject.Parse(fixturesData));

        // Fetch Standing data
        var standingData = await _footballService.GetStandingDataAsync("2023", LeagueId);
        await RefreshLeagueStanding(JObject.Parse(standingData));

        // Set loading state to false
        HelperFunctions.SetLeagueLoadingProgressState(false);
    }

    #endregion


    private async Task RefreshLeagueStanding(JObject jsonData)
    {
        var leagueStandingCollection = await _deserializeDataService.DeserializeStandingData(jsonData);

        var leagueStandingViewModel = Ioc.Default.GetRequiredService<LeagueStandingViewModel>();
        leagueStandingViewModel.StandingTeams = leagueStandingCollection;
    }

    private async Task RefreshFixtures(JObject jsonData)
    {
        var matchesCollection = await _deserializeDataService.DeserializeFixturesData(jsonData);

        var fixturesViewModel = Ioc.Default.GetRequiredService<FixturesViewModel>();
        fixturesViewModel.MatchesCollection = matchesCollection;
    }

    private async Task RefreshResults(JObject jsonData)
    {
        var matchesCollection = await _deserializeDataService.DeserializeResultsData(jsonData);

        var resultsViewModel = Ioc.Default.GetRequiredService<ResultsViewModel>();
        resultsViewModel.MatchesCollection = matchesCollection;
    }
}