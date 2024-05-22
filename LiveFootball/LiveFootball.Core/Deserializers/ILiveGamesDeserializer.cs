using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public interface ILiveGamesDeserializer
{
    public Task<List<LiveMatchModel>> Deserialize(JToken jsonData);
}