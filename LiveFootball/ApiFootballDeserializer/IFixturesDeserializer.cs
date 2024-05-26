using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface IFixturesDeserializer
{
    public Task<List<FixtureMatchModel>> Deserialize(JToken jsonData);
}