using System.Globalization;

using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public class ResultsDeserializer : IResultsDeserializer
{
    public async Task<List<ResultMatchModel>> Deserialize(JToken jsonData)
    {
        var matchesList = new List<ResultMatchModel>();
        var tasks = new List<Task<ResultMatchModel>>();
        var semaphore = new SemaphoreSlim(25); // Set the maximum concurrent tasks to 25

        while (jsonData != null)
        {
            tasks.Add(DeserializeMatchWithSemaphore(jsonData, semaphore));
            jsonData = jsonData.Next;
        }

        matchesList.AddRange(await Task.WhenAll(tasks));

        return matchesList;
    }

    private async Task<ResultMatchModel> DeserializeMatchWithSemaphore(JToken jsonResultData, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync(); // Wait for a slot to become available
        try
        {
            return await DeserializeMatch(jsonResultData);
        }
        finally
        {
            semaphore.Release(); // Release the slot when the task is completed
        }
    }

    private async Task<ResultMatchModel> DeserializeMatch(JToken jsonResultData)
    {
        var date = DateTime.TryParse(jsonResultData["fixture"]!["date"]!.ToString(), CultureInfo.CurrentCulture,
                       DateTimeStyles.None, out var dateTime)
                       ? dateTime.ToString("MMM d, HH:mm", CultureInfo.CurrentCulture)
                       : "NA";

        var homeLogo = await HelperFunctions.GetTeamLogoFromUrl(jsonResultData["teams"]!["home"]!["logo"]!.ToString());
        var awayLogo = await HelperFunctions.GetTeamLogoFromUrl(jsonResultData["teams"]!["away"]!["logo"]!.ToString());

        var homeTeam = new ExtendedTeamModel
        {
            Logo = homeLogo,
            Name = jsonResultData["teams"]!["home"]!["name"]!.ToString(),
            Goals = int.Parse(jsonResultData["goals"]!["home"]!.ToString())
        };

        var awayTeam = new ExtendedTeamModel
        {
            Logo = awayLogo,
            Name = jsonResultData["teams"]!["away"]!["name"]!.ToString(),
            Goals = int.Parse(jsonResultData["goals"]!["away"]!.ToString())
        };

        return new ResultMatchModel
        {
            Date = date,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam
        };
    }
}