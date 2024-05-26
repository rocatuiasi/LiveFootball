using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <summary>
/// Interface for deserializing standing data from JSON.
/// </summary>
public interface IStandingDeserializer
{
    /// <summary>
    /// Deserializes the given JSON token into a list of league standing team models.
    /// </summary>
    /// <param name="jsonData">The JSON data to deserialize.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="LeagueStandingTeamModel"/> as the result.</returns>
    public Task<List<LeagueStandingTeamModel>> Deserialize(JToken jsonData);
}