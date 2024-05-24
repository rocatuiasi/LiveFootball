using LiveFootball.Core.Helpers;
using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public class LeaguesDeserializer : ILeaguesDeserializer
{
    public async Task<List<MenuItemViewModel>> Deserialize(JToken jsonData)
    {
        var leagues = new List<MenuItemViewModel>();
        var tasks = new List<Task<MenuItemViewModel>>();
        var semaphore = new SemaphoreSlim(25); // Set the maximum concurrent tasks to 25
     
        foreach (var item in jsonData["response"]!)
        {
            tasks.Add(DeserializeLeagueWithSemaphore(item, semaphore));
        }
        
        leagues.AddRange(await Task.WhenAll(tasks));
        
        return leagues;
        
        // var Leagues = new List<MenuItemViewModel>
        // {
        //     new("World Cup", "1"),
        //     new("Euro Championship", "4"),
        //     new("UEFA Champions League", "2"),
        //     new("UEFA Europa League", "3"),
        //     new("Premier League", "39"),
        //     new("Championship", "40"),
        //     new("La Liga", "140"),
        //     new("Bundesliga", "78"),
        //     new("Ligue 1", "61"),
        //     new("Serie A", "135"),
        //     new("Liga I - Superliga", "283"),
        //     new("Eredivisie", "88"),
        //     new("Campeonato Brasileiro Série A", "71"),
        //     new("Primeira Liga", "94"),
        //
        // };
        // await LoadLeaguesLogosAsync(leagues);
        // return Leagues;
    }

    private async Task<MenuItemViewModel> DeserializeLeagueWithSemaphore(JToken item, SemaphoreSlim semaphore)
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
    
    private async Task<MenuItemViewModel> DeserializeLeague(JToken item)
    {
        var leagueId = item["league"]!["id"]!.ToString();
        var leagueName = item["league"]!["name"]!.ToString();
        var leagueLogoUrl = item["league"]!["logo"]!.ToString();
        
        //TODO: Add country in MenuItemViewModel for less coincidences in league names
        // var countryName = item["country"]!["name"]!.ToString();
        // var countryCode = item["country"]!["code"]!.ToString();
        // var countryFlagUrl = item["country"]!["flag"]!.ToString();
        
        var leagueLogo = await HelperFunctions.GetLeagueLogoFromUrl(leagueLogoUrl);
            
        return new MenuItemViewModel(leagueName, leagueId, leagueLogo);
    }
}