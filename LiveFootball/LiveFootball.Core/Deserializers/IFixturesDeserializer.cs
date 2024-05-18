using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public interface IFixturesDeserializer
{
    public Task<List<FixtureMatchModel>> Deserialize(JToken jsonData);
}