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

    public async Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData)
    {
        var jsonStandingData = jsonData["response"]![0]!["league"]!["standings"]![0]![0]!;

        return await _deserializerFactory.CreateStandingDeserializer().Deserialize(jsonStandingData);
    }

    public async Task<List<MatchModel>> DeserializeFixturesData(JObject jsonData)
    {
        var jsonFixtureData = jsonData["response"]![0]!;

        return await _deserializerFactory.CreateFixturesDeserializer().Deserialize(jsonFixtureData);
    }
}