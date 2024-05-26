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

public interface IFootballApiService
{
    Task<string> GetLeaguesDataAsync();
    Task<string> GetLiveGamesDataAsync();
	Task<string> GetAllGamesResultsDataAsync();
	Task<string> GetAllGamesFixturesDataAsync();
    Task<string> GetLeagueResultsDataAsync(string leagueParam);
    Task<string> GetLeagueFixturesDataAsync(string leagueParam);
    Task<string> GetLeagueStandingDataAsync(string leagueParam);
}