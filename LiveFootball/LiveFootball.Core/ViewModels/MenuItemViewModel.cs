using System.Net.Http;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LiveFootball.Core.Exceptions;
using LiveFootball.Core.Helpers;
using LiveFootball.Core.Services;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

public class MenuItemViewModel
{
    #region Backing Fields and Properties

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    public string Name { get; }
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
        // Switch current TabView to LeagueTabView
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

    #endregion


    private async Task RefreshLeagueStanding()
    {
        var leagueStandingViewModel = Ioc.Default.GetRequiredService<LeagueStandingViewModel>();
        try
        {
            // Fetch Standing data
            var standingData = await _footballService.GetStandingDataAsync("2023", LeagueId);
            var jsonData = JObject.Parse(standingData);
            // Deserialize Standing data
            var leagueStandingCollection = await _deserializeDataService.DeserializeStandingData(jsonData);

            leagueStandingViewModel.StandingTeams = leagueStandingCollection;
        }
        catch (DeserializationException)
        {
            leagueStandingViewModel.StatusMessage = "No standing data available...";
        }
        catch (HttpRequestException)
        {
            leagueStandingViewModel.StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            leagueStandingViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    private async Task RefreshFixtures()
    {
        var fixturesViewModel = Ioc.Default.GetRequiredService<FixturesViewModel>();
        try
        {
            // Fetch Fixtures data
            var fixturesData = await _footballService.GetFixturesDataAsync(LeagueId);
            var jsonData = JObject.Parse(fixturesData);
            // Deserialize Fixtures data
            var matchesCollection = await _deserializeDataService.DeserializeFixturesData(jsonData);

            fixturesViewModel.MatchesCollection = matchesCollection;
        }
        catch (DeserializationException)
        {
            fixturesViewModel.StatusMessage = "No more fixtures this season...";
        }
        catch (HttpRequestException)
        {
            fixturesViewModel.StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            fixturesViewModel.StatusMessage = "Oops, something went wrong";
        }
    }

    private async Task RefreshResults()
    {
        var resultsViewModel = Ioc.Default.GetRequiredService<ResultsViewModel>();
        try
        {
            // Fetch Results data
            var resultsData = await _footballService.GetResultsDataAsync(LeagueId);
            var jsonData = JObject.Parse(resultsData);
            // Deserialize Results data
            var matchesCollection = await _deserializeDataService.DeserializeResultsData(jsonData);

            resultsViewModel.MatchesCollection = matchesCollection;
        }
        catch (DeserializationException)
        {
            resultsViewModel.StatusMessage = "No results data available...";
        }
        catch (HttpRequestException)
        {
            resultsViewModel.StatusMessage = "Network error: either a connection problem or the API-Football is unavailable.";
        }
        catch (Exception)
        {
            resultsViewModel.StatusMessage = "Oops, something went wrong";
        }
    }
}