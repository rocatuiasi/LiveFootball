using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface IStandingDeserializer
{
    public Task<List<LeagueStandingTeamModel>> Deserialize(JToken jsonData);
}