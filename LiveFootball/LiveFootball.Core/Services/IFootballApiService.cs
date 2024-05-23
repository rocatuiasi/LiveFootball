namespace LiveFootball.Core.Services;

public interface IFootballApiService
{
    Task<string> GetLiveGamesDataAsync();

    Task<string> GetAllGamesResultsDataAsync();

    Task<string> GetAllGamesFixturesDataAsync();

    Task<string> GetLeagueResultsDataAsync(string leagueParam);

    Task<string> GetLeagueFixturesDataAsync(string leagueParam);

    Task<string> GetLeagueStandingDataAsync(string leagueParam);
}