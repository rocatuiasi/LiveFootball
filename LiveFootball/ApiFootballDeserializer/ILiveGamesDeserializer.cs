using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface ILiveGamesDeserializer
{
    public Task<List<LiveMatchModel>> Deserialize(JToken jsonData);
}