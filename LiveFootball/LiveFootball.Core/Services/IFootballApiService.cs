namespace LiveFootball.Core.Services;

public interface IFootballApiService
{
    Task<string> GetLiveGamesDataAsync();

    Task<string> GetStandingDataAsync(string seasonParam, string leagueParam);

    Task<string> GetFixturesDataAsync(string leagueParam);

    Task<string> GetResultsDataAsync(string leagueParam);
}