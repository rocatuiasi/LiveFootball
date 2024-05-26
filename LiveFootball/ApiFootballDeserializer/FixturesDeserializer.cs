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

/// <inheridoc/>
public class FixturesDeserializer : IFixturesDeserializer
{
    /// <inheridoc/>
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

    /// <summary>
    /// Deserializes a single match JSON token with semaphore control.
    /// </summary>
    /// <param name="jsonFixtureData">The JSON data of a single fixture.</param>
    /// <param name="semaphore">Semaphore to control concurrency.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="FixtureMatchModel"/> as the result.</returns>
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

    /// <summary>
    /// Deserializes a single match JSON token.
    /// </summary>
    /// <param name="jsonFixtureData">The JSON data of a single fixture.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="FixtureMatchModel"/> as the result.</returns>
    private async Task<FixtureMatchModel> DeserializeMatch(JToken jsonFixtureData)
    {
        var date = DateTime.TryParse(jsonFixtureData["fixture"]!["date"]!.ToString(), CultureInfo.CurrentCulture,


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