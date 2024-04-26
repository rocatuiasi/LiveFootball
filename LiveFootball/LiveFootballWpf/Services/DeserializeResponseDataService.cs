using LiveFootball.Core.Models;
using LiveFootball.Core.Services;

using Newtonsoft.Json.Linq;

namespace LiveFootballWpf.Services;

internal class DeserializeResponseDataService : IDeserializeResponseDataService
{
    public List<LeagueStandingTeamModel> DeserializeStandingData(JObject jsonData)
    {
        var jsonStandingData = jsonData["response"]![0]!["league"]!["standings"]![0]![0];
        var standingTeamModelList = new List<LeagueStandingTeamModel>();

        while (jsonStandingData != null)
        {
            standingTeamModelList.Add(new LeagueStandingTeamModel()
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
                Form = jsonStandingData["form"]!.ToString(),
            });

            jsonStandingData = jsonStandingData.Next;
        }

        return standingTeamModelList;
    }
}