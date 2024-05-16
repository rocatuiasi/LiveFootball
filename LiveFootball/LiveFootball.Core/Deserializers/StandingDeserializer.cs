using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public class StandingDeserializer : IStandingDeserializer
{
    public Task<List<LeagueStandingTeamModel>> Deserialize(JToken jsonData)
    {
        throw new NotImplementedException();
    }
}