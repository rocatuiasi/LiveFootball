using LiveFootball.Core.Deserializers;
using LiveFootball.Core.Exceptions;
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
        try
        {
            var jsonLiveGamesData = jsonData["response"]![0]!;

            return await _deserializerFactory.CreateLiveGamesDeserializer().Deserialize(jsonLiveGamesData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<ResultMatchModel>> DeserializeResultsData(JObject jsonData)
    {
        try
        {
            var jsonResultsData = jsonData["response"]![0]!;

            return await _deserializerFactory.CreateResultsDeserializer().Deserialize(jsonResultsData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<FixtureMatchModel>> DeserializeFixturesData(JObject jsonData)
    {
        try
        {
            var jsonFixtureData = jsonData["response"]![0]!;

            return await _deserializerFactory.CreateFixturesDeserializer().Deserialize(jsonFixtureData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData)
    {
        try
        {
            var jsonStandingData = jsonData["response"]![0]!["league"]!["standings"]![0]![0]!;

            return await _deserializerFactory.CreateStandingDeserializer().Deserialize(jsonStandingData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }
}