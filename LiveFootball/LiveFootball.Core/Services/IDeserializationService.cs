using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Services;

/// <summary>
/// Represents a service for deserializing JSON data into various models.
/// </summary>
public interface IDeserializationService
{
    /// <summary>
    /// Deserialize JSON data into a list of menu item models representing leagues.
    /// </summary>
    Task<List<MenuItemModel>> DeserializeLeaguesData(JObject jsonData);

    /// <summary>
    /// Deserialize JSON data into a list of live match models.
    /// </summary>
    Task<List<LiveMatchModel>> DeserializeLiveGamesData(JObject jsonData);

    /// <summary>
    /// Deserialize JSON data into a list of result match models.
    /// </summary>
    Task<List<ResultMatchModel>> DeserializeResultsData(JObject jsonData);

    /// <summary>
    /// Deserialize JSON data into a list of fixture match models.
    /// </summary>
    Task<List<FixtureMatchModel>> DeserializeFixturesData(JObject jsonData);

    /// <summary>
    /// Deserialize JSON data into a list of league standing team models.
    /// </summary>
    Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData);
}