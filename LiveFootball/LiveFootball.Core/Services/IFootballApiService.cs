namespace LiveFootball.Core.Services;

public interface IFootballApiService
{
    Task<string> GetStandingDataAsync(string seasonParam, string leagueParam);
    Task<string> GetFixturesDataAsync(string leagueParam);
}