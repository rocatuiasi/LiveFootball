/**************************************************************************
 *                                                                        * 
 *  File:        IFootballApiService.cs                                   *
 *  Description: LiveFootball.Core.Services Library                      *
 *               Interface for fetching data from the football API.       *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
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