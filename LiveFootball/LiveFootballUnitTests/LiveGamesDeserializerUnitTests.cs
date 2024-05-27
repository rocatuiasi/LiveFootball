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
[TestSubject(typeof(LiveGamesDeserializer))]
[TestCategory("Unit")]
public class LiveGamesDeserializerUnitTests
{
    private List<LiveMatchModel> _liveGames;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var liveGamesDeserializer = new LiveGamesDeserializer();
        var fileContent = await Helper.ReadFileFromAssetsAsync("get-fixtures-live-sample-response.json");
        var jsonData = JObject.Parse(fileContent);
        var jsonResultsData = jsonData["response"]![0]!;
        _liveGames = await liveGamesDeserializer.Deserialize(jsonResultsData);
    }

    [TestMethod]
    public void AssertEqualFixtureMatchModel_FirstFixture()
    {
        // Arrange
        var firstLiveMatchModel = _liveGames.First();

        // Act & Assert
        var areEqual = CheckEqual(firstLiveMatchModel, new LiveMatchModel
        {
            HomeTeam = new ExtendedTeamModel { Name = "Radnicki Pirot", Goals = 0 },
            AwayTeam = new ExtendedTeamModel { Name = "Borac Cacak", Goals = 0 },
            Status = "7'"
        });
        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertEqualLiveMatchModel_LastFixture()
    {
        var lastLiveMatchModel = _liveGames.Last();

        var areEqual = CheckEqual(lastLiveMatchModel, new LiveMatchModel
        {
            HomeTeam = new ExtendedTeamModel { Name = "Etoile DU Sahel", Goals = 3 },
            AwayTeam = new ExtendedTeamModel { Name = "JS Kairouanaise", Goals = 1 },
            Status = "HT"
        });

        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertNotEqualLiveMatchModel()
    {
        var liveMatchModel = new LiveMatchModel
        {
            HomeTeam = new ExtendedTeamModel { Name = "InvalidHome", Goals = 1 },
            AwayTeam = new ExtendedTeamModel { Name = "InvalidAway", Goals = 0 },
            Status = "FT"
        };

        Assert.IsFalse(CheckEqual(liveMatchModel, _liveGames.First()));
    }

    private bool CheckEqual(LiveMatchModel expected, LiveMatchModel actual)
    {
        return expected.HomeTeam.Name == actual.HomeTeam.Name &&
               expected.HomeTeam.Goals == actual.HomeTeam.Goals &&
               expected.AwayTeam.Name == actual.AwayTeam.Name &&
               expected.AwayTeam.Goals == actual.AwayTeam.Goals &&
               expected.Status == actual.Status;
    }
}