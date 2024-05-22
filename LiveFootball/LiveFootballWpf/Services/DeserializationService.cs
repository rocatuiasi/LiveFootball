using LiveFootball.Core.Deserializers;
using LiveFootball.Core.Models;
using LiveFootball.Core.Services;

using Newtonsoft.Json.Linq;

namespace LiveFootballWpf.Services;

public class DeserializationService : IDeserializationService
{
    private readonly IDeserializerFactory _deserializerFactory;

    public DeserializationService(IDeserializerFactory deserializerFactory)
    {
        _deserializerFactory = deserializerFactory;
    }

    public async Task<List<LiveMatchModel>> DeserializeLiveGamesData(JObject jsonData)
    {
        var jsonLiveGamesData = jsonData["response"]![0]!;

        return await _deserializerFactory.CreateLiveGamesDeserializer().Deserialize(jsonLiveGamesData);
    }

    public async Task<List<ResultMatchModel>> DeserializeResultsData(JObject jsonData)
    {
        var jsonResultsData = jsonData["response"]![0]!;

        return await _deserializerFactory.CreateResultsDeserializer().Deserialize(jsonResultsData);
    }

    public async Task<List<FixtureMatchModel>> DeserializeFixturesData(JObject jsonData)
    {
        var jsonFixtureData = jsonData["response"]![0]!;

        return await _deserializerFactory.CreateFixturesDeserializer().Deserialize(jsonFixtureData);
    }

    public async Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData)
    {
        var jsonStandingData = jsonData["response"]![0]!["league"]!["standings"]![0]![0]!;

        return await _deserializerFactory.CreateStandingDeserializer().Deserialize(jsonStandingData);
    }
}