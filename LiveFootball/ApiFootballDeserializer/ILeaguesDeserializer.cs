using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface ILeaguesDeserializer
{
    public Task<List<MenuItemViewModel>> Deserialize(JToken jsonData);
}