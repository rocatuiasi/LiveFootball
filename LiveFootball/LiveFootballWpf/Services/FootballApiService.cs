using System.Net.Http;

using LiveFootball.Core.Services;

namespace LiveFootballWpf.Services;

public sealed class FootballApiService : IFootballApiService
{
    private const string BaseRequestUri = "https://api-football-v1.p.rapidapi.com/v3/";

    private readonly HttpClient _client;
    private readonly string _apiKey = "2b12d424acmsh6a84ed754ea566fp1e5482jsn96ab0659b63b";

    public FootballApiService()
    {
        _client = new HttpClient();
    }

    public async Task<string> GetLiveGamesDataAsync()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/fixtures?live=all"),
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return body;
    }

    public async Task<string> GetAllGamesResultsDataAsync()
    {
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?date={currentDate}&status=FT"),
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return body;
    }

    public async Task<string> GetAllGamesFixturesDataAsync()
    {
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?date={currentDate}&status=NS"),
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return body;
    }

    public async Task<string> GetLeagueResultsDataAsync(string leagueParam)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?league={leagueParam}&last=30"),
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return body;
    }

    public async Task<string> GetLeagueFixturesDataAsync(string leagueParam)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?league={leagueParam}&next=99"),
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return body;
    }

    public async Task<string> GetLeagueStandingDataAsync(string leagueParam)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{BaseRequestUri}standings?season=2023&league={leagueParam}"),
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return body;
    }
}