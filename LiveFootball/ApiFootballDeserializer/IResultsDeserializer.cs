using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <summary>
/// Interface for deserializing result data from JSON.
/// </summary>
public interface IResultsDeserializer
{
    /// <summary>
    /// Deserializes the given JSON token into a list of result match models.
    /// </summary>
    /// <param name="jsonData">The JSON data to deserialize.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="ResultMatchModel"/> as the result.</returns>
    public Task<List<ResultMatchModel>> Deserialize(JToken jsonData);
}