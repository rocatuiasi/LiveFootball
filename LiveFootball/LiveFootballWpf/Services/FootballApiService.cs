using System.IO;
using System.Net.Http;
using LiveFootball.Core.Services;

namespace LiveFootballWpf.Services;

public sealed class FootballApiService : IFootballApiService
{
    private const string BaseRequestUri = "https://api-football-v1.p.rapidapi.com/v3/";
    private const string ApiGetLeaguesFileName = "get-leagues.json";

    private readonly HttpClient _client;
    private string _apiKey = "46b11dfc43msh7b2750b66a95a81p112f79jsne701175ae260";
    private int _currentSeason = 2023;

    public FootballApiService()
    {
        _client = new HttpClient();
    }
    
    public async Task<string> GetLeaguesDataAsync()
    {
        string jsonData;
        try
        {
            // Read from file
            jsonData = await File.ReadAllTextAsync(ApiGetLeaguesFileName);
            Console.WriteLine($"Successfully read from file {ApiGetLeaguesFileName}");
        }
        catch (Exception)
        {
            // If file is corrupted, fetch from API
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/leagues?season={_currentSeason}"),
                Headers =
                {
                    { "X-RapidAPI-Key", _apiKey },
                    { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
                }
            };

            using var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            jsonData = await response.Content.ReadAsStringAsync();

            // Write to file for future use to avoid API calls
            await File.WriteAllTextAsync(ApiGetLeaguesFileName, jsonData);
        }

        return jsonData;
    }

    public async Task<string> GetLiveGamesDataAsync()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?live=all"),
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
    
    public async Task<string> GetResultsDataAsync(string leagueParam)
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

    public async Task<string> GetFixturesDataAsync(string leagueParam)
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

    public async Task<string> GetStandingDataAsync(string seasonParam, string leagueParam)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{BaseRequestUri}standings?season={seasonParam}&league={leagueParam}"),
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