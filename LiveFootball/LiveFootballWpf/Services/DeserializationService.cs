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

    public List<LeagueStandingTeamModel> DeserializeStandingData(JObject jsonData)
    {
        var jsonStandingData = jsonData["response"]![0]!["league"]!["standings"]![0]![0];
        var standingTeamModelList = new List<LeagueStandingTeamModel>();

        while (jsonStandingData != null)
        {
            standingTeamModelList.Add(new LeagueStandingTeamModel
            {
                Position = int.Parse(jsonStandingData["rank"]!.ToString()),
                Club = jsonStandingData["team"]!["name"]!.ToString(),
                MatchesPlayed = int.Parse(jsonStandingData["all"]!["played"]!.ToString()),
                MatchesWon = int.Parse(jsonStandingData["all"]!["win"]!.ToString()),
                MatchesDrawn = int.Parse(jsonStandingData["all"]!["draw"]!.ToString()),
                MatchesLost = int.Parse(jsonStandingData["all"]!["lose"]!.ToString()),
                GoalsFor = int.Parse(jsonStandingData["all"]!["goals"]!["for"]!.ToString()),
                GoalsAgainst = int.Parse(jsonStandingData["all"]!["goals"]!["against"]!.ToString()),
                GoalDifference = int.Parse(jsonStandingData["goalsDiff"]!.ToString()),
                Points = int.Parse(jsonStandingData["points"]!.ToString()),
                Form = jsonStandingData["form"]!.ToString()
            });

            jsonStandingData = jsonStandingData.Next;
        }

        return standingTeamModelList;
    }

    public async Task<List<MatchModel>> DeserializeFixturesData(JObject jsonData)
    {
        var jsonFixtureData = jsonData["response"]![0]!;
        return await _deserializerFactory.CreateFixturesDeserializer().Deserialize(jsonFixtureData);
    }
}