using LiveFootball.Core.Models;
using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface ILeaguesDeserializer
{
    public Task<List<MenuItemModel>> Deserialize(JToken jsonData);
}