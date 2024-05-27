using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFootballDeserializer;
using JetBrains.Annotations;
using LiveFootball.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Helper = LiveFootballUnitTests.DeserializerUnitTestHelper;

namespace LiveFootballUnitTests;

[TestClass]
[TestSubject(typeof(StandingDeserializer))]
[TestCategory("Unit")]
public class StandingDeserializerUnitTests
{
    private List<LeagueStandingTeamModel> _standings;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var standingDeserializer = new StandingDeserializer();
        var fileContent = await Helper.ReadFileFromAssetsAsync("get-standings-sample-response.json");
        var jsonData = JObject.Parse(fileContent);
        var jsonResultsData = jsonData["response"]![0]!["league"]!["standings"]![0]![0]!;
        _standings = await standingDeserializer.Deserialize(jsonResultsData);
    }

    [TestMethod]
    public void AssertEqualFixtureMatchModel_FirstFixture()
    {
        var firstLeague = _standings.First();

        var areEqual = CheckEqual(firstLeague, new LeagueStandingTeamModel
        {
            Position = 1,
            Club = "Manchester City",
            MatchesPlayed = 38,
            MatchesWon = 28,
            MatchesDrawn = 7,
            MatchesLost = 3,
            GoalsFor = 96,
            GoalsAgainst = 34,
            GoalDifference = 62,
            Points = 91,
            Form = "WWWWW"
        });
        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertEqualLiveMatchModel_LastFixture()
    {
        var lastLeague = _standings.Last();

        var areEqual = CheckEqual(lastLeague, new LeagueStandingTeamModel
        {
            Position = 20,
            Club = "Sheffield Utd",
            MatchesPlayed = 38,
            MatchesWon = 3,
            MatchesDrawn = 7,
            MatchesLost = 28,
            GoalsFor = 35,
            GoalsAgainst = 104,
            GoalDifference = -69,
            Points = 16,
            Form = "LLLLL"
        });

        Assert.IsTrue(areEqual);

        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertNotEqualLiveMatchModel()
    {
        var league = new LeagueStandingTeamModel
        {
            Position = 3,
            Club = "Club 3",
            MatchesPlayed = 10,
            MatchesWon = 3,
            MatchesDrawn = 4,
            MatchesLost = 3,
            GoalsFor = 10,
            GoalsAgainst = 15,
            GoalDifference = -5,
            Points = 13,
            Form = "DLWLD"
        };

        Assert.IsFalse(CheckEqual(league, _standings.First()));
    }

    private bool CheckEqual(LeagueStandingTeamModel expected, LeagueStandingTeamModel actual)
    {
        return expected.Position == actual.Position &&
               expected.Club == actual.Club &&
               expected.MatchesPlayed == actual.MatchesPlayed &&
               expected.MatchesWon == actual.MatchesWon &&
               expected.MatchesDrawn == actual.MatchesDrawn &&
               expected.MatchesLost == actual.MatchesLost &&
               expected.GoalsFor == actual.GoalsFor &&
               expected.GoalsAgainst == actual.GoalsAgainst &&
               expected.GoalDifference == actual.GoalDifference &&
               expected.Points == actual.Points &&
               expected.Form == actual.Form;
    }
}