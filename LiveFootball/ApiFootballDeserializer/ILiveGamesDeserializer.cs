using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <summary>
/// Interface for deserializing live game data from JSON.
/// </summary>
public interface ILiveGamesDeserializer
{
    /// <summary>
    /// Deserializes the given JSON token into a list of live match models.
    /// </summary>
    /// <param name="jsonData">The JSON data to deserialize.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="LiveMatchModel"/> as the result.</returns>
    public Task<List<LiveMatchModel>> Deserialize(JToken jsonData);
}