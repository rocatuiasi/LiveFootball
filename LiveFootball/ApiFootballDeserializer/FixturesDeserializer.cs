/**************************************************************************
 *                                                                        * 
 *  File:        FixturesDeserializer.cs                                  *
 *  Description: ApiFootballDeserializer Library                          *
 *               Deserializes JSON data into fixture match models         *
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
using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public class FixturesDeserializer : IFixturesDeserializer
{
    public async Task<List<FixtureMatchModel>> Deserialize(JToken jsonData)
    {
        var matchesList = new List<FixtureMatchModel>();
        var tasks = new List<Task<FixtureMatchModel>>();
        var semaphore = new SemaphoreSlim(25); // Set the maximum concurrent tasks to 25

        while (jsonData != null)
        {
            tasks.Add(DeserializeMatchWithSemaphore(jsonData, semaphore));
            jsonData = jsonData.Next;
        }

        matchesList.AddRange(await Task.WhenAll(tasks));

        return matchesList;
    }

    private async Task<FixtureMatchModel> DeserializeMatchWithSemaphore(JToken jsonFixtureData, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync(); // Wait for a slot to become available
        try
        {
            return await DeserializeMatch(jsonFixtureData);
        }
        finally
        {
            semaphore.Release(); // Release the slot when the task is completed
        }
    }

    private async Task<FixtureMatchModel> DeserializeMatch(JToken jsonFixtureData)
    {
        var date = DateTime.TryParse(jsonFixtureData["fixture"]!["date"]!.ToString(), CultureInfo.CurrentCulture,
                       DateTimeStyles.None, out var dateTime)
                       ? dateTime.ToString("MMM d, HH:mm", CultureInfo.CurrentCulture)
                       : "NA";


        var homeLogo = await HelperFunctions.GetTeamLogoFromUrl(jsonFixtureData["teams"]!["home"]!["logo"]!.ToString());
        var awayLogo = await HelperFunctions.GetTeamLogoFromUrl(jsonFixtureData["teams"]!["away"]!["logo"]!.ToString());

        var homeTeam = new TeamModel
        {
            Logo = homeLogo,
            Name = jsonFixtureData["teams"]!["home"]!["name"]!.ToString()
        };
        var awayTeam = new TeamModel
        {
            Logo = awayLogo,
            Name = jsonFixtureData["teams"]!["away"]!["name"]!.ToString()
        };

        return new FixtureMatchModel
        {
            Date = date,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
    }
}