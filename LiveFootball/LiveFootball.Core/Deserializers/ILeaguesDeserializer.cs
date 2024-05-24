using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public interface ILeaguesDeserializer
{
    public Task<List<MenuItemViewModel>> Deserialize(JToken jsonData);
}