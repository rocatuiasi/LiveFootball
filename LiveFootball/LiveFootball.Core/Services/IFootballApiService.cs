namespace LiveFootball.Core.Services;

/// <summary>
/// Represents a service for interacting with a football API to retrieve data.
/// </summary>
public interface IFootballApiService
{
    /// <summary>
    /// Retrieves data for all available leagues.
    /// </summary>
    Task<string> GetLeaguesDataAsync();

    /// <summary>
    /// Retrieves live games data.
    /// </summary>
    Task<string> GetLiveGamesDataAsync();

    /// <summary>
    /// Retrieves data for all games results.
    /// </summary>
    Task<string> GetAllGamesResultsDataAsync();

    /// <summary>
    /// Retrieves data for all games fixtures.
    /// </summary>
    Task<string> GetAllGamesFixturesDataAsync();

    /// <summary>
    /// Retrieves results data for a specific league.
    /// </summary>
    /// <param name="leagueParam">The parameter specifying the league.</param>
    Task<string> GetLeagueResultsDataAsync(string leagueParam);

    /// <summary>
    /// Retrieves fixtures data for a specific league.
    /// </summary>
    /// <param name="leagueParam">The parameter specifying the league.</param>
    Task<string> GetLeagueFixturesDataAsync(string leagueParam);
    
    /// <summary>
    /// Retrieves standing data for a specific league.
    /// </summary>
    /// <param name="leagueParam">The parameter specifying the league.</param>
    Task<string> GetLeagueStandingDataAsync(string leagueParam);
}