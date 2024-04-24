using System.Net.Http;

namespace LiveFootball.Core.Services;

public sealed class FootballApiService : IFootballApiService
{
    private readonly HttpClient _client;

    public FootballApiService()
    {
        _client = new HttpClient();
    }

    public async Task<string> GetStandingDataAsync()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/fixtures?date=2021-01-29"),
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