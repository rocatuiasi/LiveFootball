using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

/// <summary>
/// Interface for deserializing league data from JSON.
/// </summary>
public interface ILeaguesDeserializer
{
    /// <summary>
    /// Deserializes the given JSON token into a list of menu item models representing leagues.
    /// </summary>
    /// <param name="jsonData">The JSON data to deserialize.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="MenuItemModel"/> as the result.</returns>
    public Task<List<MenuItemModel>> Deserialize(JToken jsonData);
}