/**************************************************************************
 *                                                                        * 
 *  File:        FootballApiService.cs                                    *
 *  Description: LiveFootballWpf.Services Library                         *
 *               Service for interacting with the football API.           *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using System.Globalization;
using System.IO;
using System.Net.Http;

using LiveFootball.Core.Services;

namespace LiveFootballWpf.Services;
/// <summary>
/// Represents a service for interacting with a football API to retrieve data.
/// </summary>
public sealed class FootballApiService : IFootballApiService
{
    private const string BaseRequestUri = "https://api-football-v1.p.rapidapi.com/v3/";
    private const string ApiGetLeaguesFileName = "get-leagues.json";

    private readonly HttpClient _client;
    private string _apiKey = "daa3074446mshfbd2b9311fcfc28p11f586jsn7f03347fcef0";
    private int _currentSeason = 2023;

    /// <summary>
    /// Initializes a new instance of the <see cref="FootballApiService"/> class.
    /// </summary>
    public FootballApiService()
    {
        _client = new HttpClient();
    }

    private async Task<string> BasicGetRequest(Uri requestUri)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = requestUri,
            Headers =
            {
                { "X-RapidAPI-Key", _apiKey },
                { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <inheritdoc/>
    public async Task<string> GetLeaguesDataAsync()
    {
        string body;
        try
        {
            // Read from file
            body = await File.ReadAllTextAsync(ApiGetLeaguesFileName);
            Console.WriteLine($"Successfully read from file {ApiGetLeaguesFileName}");
        }
        catch (Exception)
        {
            // If file is corrupted, fetch from API
            body = await BasicGetRequest(new Uri($"https://api-football-v1.p.rapidapi.com/v3/leagues?season={_currentSeason}"));

            // Write to file for future use to avoid API calls
            try
            {
                await File.WriteAllTextAsync(ApiGetLeaguesFileName, body);
            }
            catch
            {
                // ignored
            }
        }

        return body;
    }

    /// <inheritdoc/>
    public async Task<string> GetLiveGamesDataAsync()
    {
        return await BasicGetRequest(new Uri("https://api-football-v1.p.rapidapi.com/v3/fixtures?live=all"));
    }

    /// <inheritdoc/>
    public async Task<string> GetAllGamesResultsDataAsync()
    {
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        return await BasicGetRequest(new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?date={currentDate}&status=FT"));
    }

    /// <inheritdoc/>
    public async Task<string> GetAllGamesFixturesDataAsync()
    {
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        return await BasicGetRequest(new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?date={currentDate}&status=NS"));
    }

    /// <inheritdoc/>
    public async Task<string> GetLeagueResultsDataAsync(string leagueParam)
    {
        return await BasicGetRequest(new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?league={leagueParam}&last=30"));
    }

    /// <inheritdoc/>
    public async Task<string> GetLeagueFixturesDataAsync(string leagueParam)
    {
        return await BasicGetRequest(new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?league={leagueParam}&next=99"));
    }

    /// <inheritdoc/>
    public async Task<string> GetLeagueStandingDataAsync(string leagueParam)
    {
        return await BasicGetRequest(new Uri($"{BaseRequestUri}standings?season={_currentSeason}&league={leagueParam}"));
    }
}
