using Newtonsoft.Json.Linq;
using System.Windows.Input;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using LiveFootball.Core.Services;
using Newtonsoft.Json;

namespace LiveFootball.Core.ViewModels;

public class MenuItemViewModel
{
    #region Backing Fields and Properties

    private readonly IFootballApiService _footballService;
    private readonly IDeserializeResponseDataService _deserializeDataService;

    public string Name { get; set; }

    private string LeagueId { get; set; }

    #endregion


    #region ICommand

    public ICommand FetchDataCommand => new AsyncRelayCommand(FetchData);

    #endregion


    #region Constructors

    public MenuItemViewModel(string name, string leagueId, IFootballApiService? footballApiService = null, IDeserializeResponseDataService? deserializeDataService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();
        _deserializeDataService = deserializeDataService ?? Ioc.Default.GetRequiredService<IDeserializeResponseDataService>();

        Name = name;
        LeagueId = leagueId;
    }

    #endregion


    #region ICommand Execution 

    private async Task FetchData()
    {
        var data = await _footballService.GetStandingDataAsync("2023", LeagueId);

        SetLeagueStanding(JObject.Parse(data));

        // TODO: Fixtures and Results from API
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
}