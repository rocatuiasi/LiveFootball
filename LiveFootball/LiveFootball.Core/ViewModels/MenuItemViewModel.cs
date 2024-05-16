using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using LiveFootball.Core.Services;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.ViewModels;

public class MenuItemViewModel
{
    #region Backing Fields and Properties

    private readonly IFootballApiService _footballService;
    private readonly IDeserializationService _deserializeDataService;

    public string Name { get; set; }

    private string LeagueId { get; set; }

    #endregion


    #region ICommand

    public ICommand FetchDataCommand => new AsyncRelayCommand(FetchData);

    #endregion


    #region Constructors

    public MenuItemViewModel(string name, string leagueId, IFootballApiService? footballApiService = null,
        IDeserializationService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService =
            deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializationService>();

        Name = name;
        LeagueId = leagueId;
    }

    #endregion


    #region ICommand Execution

    private async Task FetchData()
    {
        // Set loading state to true
        Ioc.Default.GetRequiredService<FixturesViewModel>().IsLoading = true;
        
        // Fetch Standing data
        var standingData = await _footballService.GetStandingDataAsync("2023", LeagueId);
        SetLeagueStanding(JObject.Parse(standingData));

        // Fetch Fixtures data
        var fixturesData = await _footballService.GetFixturesDataAsync(LeagueId);
        await RefreshFixtures(JObject.Parse(fixturesData));

        // TODO: Results from API

        // Set loading state to false
        Ioc.Default.GetRequiredService<FixturesViewModel>().IsLoading = false;
    }

    #endregion


    private void SetLeagueStanding(JObject jsonData)
    {
        var leagueStandingViewModel = Ioc.Default.GetRequiredService<LeagueStandingViewModel>();
        leagueStandingViewModel.StandingTeams.Clear();

        foreach (var standingTeam in _deserializeDataService.DeserializeStandingData(jsonData))
        {
            leagueStandingViewModel.StandingTeams.Add(standingTeam);
        }
    }

    private async Task RefreshFixtures(JObject jsonData)
    {
        var name = jsonData["response"]![0]!["league"]!["name"]!.ToString();
        var matchesCollection = await _deserializeDataService.DeserializeFixturesData(jsonData);
        var leagueFixtures = new LeagueExpanderViewModel
        {
            Name = $"{name}: {matchesCollection.Count}",
            MatchesCollection = matchesCollection
        };
        
        var fixturesViewModel = Ioc.Default.GetRequiredService<FixturesViewModel>();
        fixturesViewModel.League = leagueFixtures;
    }
}