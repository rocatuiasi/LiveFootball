using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public interface IStandingDeserializer
{
    public Task<List<LeagueStandingTeamModel>> Deserialize(JToken jsonData);
}