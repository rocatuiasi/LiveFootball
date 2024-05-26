using LiveFootball.Core.Helpers;
using LiveFootball.Core.Models;
using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public class LeaguesDeserializer : ILeaguesDeserializer
{
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