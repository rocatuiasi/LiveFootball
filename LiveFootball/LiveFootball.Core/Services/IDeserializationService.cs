using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Services;

public interface IDeserializationService
{
    Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData);

    Task<List<MatchModel>> DeserializeFixturesData(JObject jsonData);
}