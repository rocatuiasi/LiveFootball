using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Services;

public interface IDeserializationService
{
    Task<List<LiveMatchModel>> DeserializeLiveGamesData(JObject jsonData);

    Task<List<ResultMatchModel>> DeserializeResultsData(JObject jsonData);

    Task<List<FixtureMatchModel>> DeserializeFixturesData(JObject jsonData);

    Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData);
}