/**************************************************************************
 *                                                                        * 
 *  File:        LiveGamesDeserializer.cs                                 *
 *  Description: ApiFootballDeserializer Library                          *
 *               Deserializes JSON data into live match models            *
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

public class LiveGamesDeserializer : ILiveGamesDeserializer
{
    public async Task<List<LiveMatchModel>> Deserialize(JToken jsonData)
    {
        var matchesList = new List<LiveMatchModel>();
        var tasks = new List<Task<LiveMatchModel>>();
        var semaphore = new SemaphoreSlim(25); // Set the maximum concurrent tasks to 25

        while (jsonData != null)
        {
            tasks.Add(DeserializeLiveMatchWithSemaphore(jsonData, semaphore));
            jsonData = jsonData.Next;
        }

        matchesList.AddRange(await Task.WhenAll(tasks));

        return matchesList;
    }

    private async Task<LiveMatchModel> DeserializeLiveMatchWithSemaphore(
        JToken jsonLiveMatchData, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync(); // Wait for a slot to become available
        try
        {
            return await DeserializeMatch(jsonLiveMatchData);
        }
        finally
        {
            semaphore.Release(); // Release the slot when the task is completed
        }
    }

    private async Task<LiveMatchModel> DeserializeMatch(JToken jsonLiveMatchData)
    {
        var matchStatusJson = jsonLiveMatchData["fixture"]!["status"]!;
        var matchStatusShortString = matchStatusJson["short"]!.ToString();

        string matchStatus;

        if (matchStatusShortString.Equals("1H") || matchStatusShortString.Equals("2H"))
            matchStatus = $"{matchStatusJson["elapsed"]!}'";
        else
            matchStatus = matchStatusShortString;

        var homeLogo =
            await HelperFunctions.GetTeamLogoFromUrl(jsonLiveMatchData["teams"]!["home"]!["logo"]!.ToString());
        var awayLogo =
            await HelperFunctions.GetTeamLogoFromUrl(jsonLiveMatchData["teams"]!["away"]!["logo"]!.ToString());

        var homeTeam = new ExtendedTeamModel
        {
            Logo = homeLogo,
            Name = jsonLiveMatchData["teams"]!["home"]!["name"]!.ToString(),
            Goals = int.Parse(jsonLiveMatchData["goals"]!["home"]!.ToString())
        };

        var awayTeam = new ExtendedTeamModel
        {
            Logo = awayLogo,
            Name = jsonLiveMatchData["teams"]!["away"]!["name"]!.ToString(),
            Goals = int.Parse(jsonLiveMatchData["goals"]!["away"]!.ToString())
        };

        return new LiveMatchModel
        {
            Status = matchStatus,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
    }
}