namespace LiveFootball.Core.Services;

public interface IFootballApiService
{
    string GetLeaguesData();
    Task<string> GetLiveGamesDataAsync();
    Task<string> GetStandingDataAsync(string seasonParam, string leagueParam);
    Task<string> GetFixturesDataAsync(string leagueParam);
    Task<string> GetResultsDataAsync(string leagueParam);
}