using System.Globalization;

using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

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