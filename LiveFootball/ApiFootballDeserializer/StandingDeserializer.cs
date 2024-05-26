/**************************************************************************
 *                                                                        * 
 *  File:        StandingDeserializer.cs                                  *
 *  Description: ApiFootballDeserializer Library                          *
 *               Deserializes JSON data into league standing team models  *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <summary>
/// Class for deserializing standing data from JSON.
/// </summary>
public class StandingDeserializer : IStandingDeserializer
{
    /// <inheridoc/>
    public async Task<List<LeagueStandingTeamModel>> Deserialize(JToken jsonData)
    {
        var standingTeamList = new List<LeagueStandingTeamModel>();
        var tasks = new List<Task<LeagueStandingTeamModel>>();
        var semaphore = new SemaphoreSlim(25); // Set the maximum concurrent tasks to 25

        while (jsonData != null)
        {
            tasks.Add(DeserializeStandingWithSemaphore(jsonData, semaphore));
            jsonData = jsonData.Next;
        }

        standingTeamList.AddRange(await Task.WhenAll(tasks));

        return standingTeamList;
    }

    // <summary>
    /// Deserializes a single standing JSON token with semaphore control.
    /// </summary>
    /// <param name="jsonStandingData">The JSON data of a single standing entry.</param>
    /// <param name="semaphore">Semaphore to control concurrency.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="LeagueStandingTeamModel"/> as the result.</returns>
    private async Task<LeagueStandingTeamModel> DeserializeStandingWithSemaphore(
        JToken jsonStandingData, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync(); // Wait for a slot to become available
        try
        {
            return await DeserializeStanding(jsonStandingData);
        }
        finally
        {
            semaphore.Release(); // Release the slot when the task is completed
        }
    }

    /// <summary>
    /// Deserializes a single standing JSON token.
    /// </summary>
    /// <param name="jsonStandingData">The JSON data of a single standing entry.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="LeagueStandingTeamModel"/> as the result.</returns>
    private async Task<LeagueStandingTeamModel> DeserializeStanding(JToken jsonStandingData)
    {
        var teamLogo = await HelperFunctions.GetTeamLogoFromUrl(jsonStandingData["team"]!["logo"]!.ToString());

        return new LeagueStandingTeamModel
        {
            Position = int.Parse(jsonStandingData["rank"]!.ToString()),
            Logo = teamLogo,
            Club = jsonStandingData["team"]!["name"]!.ToString(),
            MatchesPlayed = int.Parse(jsonStandingData["all"]!["played"]!.ToString()),
            MatchesWon = int.Parse(jsonStandingData["all"]!["win"]!.ToString()),
            MatchesDrawn = int.Parse(jsonStandingData["all"]!["draw"]!.ToString()),
            MatchesLost = int.Parse(jsonStandingData["all"]!["lose"]!.ToString()),
            GoalsFor = int.Parse(jsonStandingData["all"]!["goals"]!["for"]!.ToString()),
            GoalsAgainst = int.Parse(jsonStandingData["all"]!["goals"]!["against"]!.ToString()),
            GoalDifference = int.Parse(jsonStandingData["goalsDiff"]!.ToString()),
            Points = int.Parse(jsonStandingData["points"]!.ToString()),
            Form = jsonStandingData["form"]!.ToString()
        };
    }
}