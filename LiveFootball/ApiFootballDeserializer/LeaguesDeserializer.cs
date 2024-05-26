using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <inheridoc/>
public class LeaguesDeserializer : ILeaguesDeserializer
{
    /// <inheridoc/>
    public async Task<List<MenuItemModel>> Deserialize(JToken jsonData)
    {
        var leagues = new List<MenuItemModel>();
        var tasks = new List<Task<MenuItemModel>>();
        var semaphore = new SemaphoreSlim(25); // Set the maximum concurrent tasks to 25
     
        foreach (var item in jsonData["response"]!)
        {
            tasks.Add(DeserializeLeagueWithSemaphore(item, semaphore));
        }
        
        leagues.AddRange(await Task.WhenAll(tasks));
        
        return leagues;
    }

    /// <summary>
    /// Deserializes a single league JSON token with semaphore control.
    /// </summary>
    /// <param name="item">The JSON data of a single league.</param>
    /// <param name="semaphore">Semaphore to control concurrency.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="MenuItemModel"/> as the result.</returns>
    private async Task<MenuItemModel> DeserializeLeagueWithSemaphore(JToken item, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync(); // Wait for a slot to become available
        try
        {
            return await DeserializeLeague(item);
        }
        finally
        {
            semaphore.Release(); // Release the slot when the task is completed
        }
    }

    /// <summary>
    /// Deserializes a single league JSON token.
    /// </summary>
    /// <param name="item">The JSON data of a single league.</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="MenuItemModel"/> as the result.</returns>
    private async Task<MenuItemModel> DeserializeLeague(JToken item)
    {
        var leagueId = item["league"]!["id"]!.ToString();
        var leagueName = item["league"]!["name"]!.ToString();
        var leagueLogoUrl = item["league"]!["logo"]!.ToString();
    
        //TODO: Add country in MenuItemViewModel for less coincidences in league names
        // var countryName = item["country"]!["name"]!.ToString();
        // var countryCode = item["country"]!["code"]!.ToString();
        // var countryFlagUrl = item["country"]!["flag"]!.ToString();
    
        var leagueLogo = await HelperFunctions.GetLeagueLogoFromUrl(leagueLogoUrl);
        
        return new MenuItemModel(leagueName, leagueId, leagueLogo);
    }
}