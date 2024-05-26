using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <summary>
/// Interface for deserializing fixture data from JSON.
/// </summary>
public interface IFixturesDeserializer
{
    /// <summary>
    /// Deserializes the given JSON token into a list of fixture match models.
    /// </summary>
    /// <param name="jsonData">The JSON data to deserialize.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="FixtureMatchModel"/> as the result.</returns>
    public Task<List<FixtureMatchModel>> Deserialize(JToken jsonData);
}