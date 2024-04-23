namespace LiveFootball.Core.Services;

public interface IFootballApiService
{
    Task<string> GetStandingDataAsync();
}