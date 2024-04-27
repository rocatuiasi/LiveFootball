using System.Net.Http;

using LiveFootball.Core.Services;

namespace LiveFootballWpf.Services;

public sealed class FootballApiService : IFootballApiService
{
    private const string BaseRequestUri = "https://api-football-v1.p.rapidapi.com/v3/";

    private readonly HttpClient _client;

    public FootballApiService()
    {
        _client = new HttpClient();
    }

    public async Task<string> GetStandingDataAsync(string seasonParam, string leagueParam)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{BaseRequestUri}standings?season={seasonParam}&league={leagueParam}"),
            Headers =
            {
                { "X-RapidAPI-Key", "2b12d424acmsh6a84ed754ea566fp1e5482jsn96ab0659b63b" },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using (var response = await _client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return body;
        }
    }
}