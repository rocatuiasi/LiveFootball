using System;
using System.Collections.Generic;
using System.Globalization;
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
[TestSubject(typeof(FixturesDeserializer))]
[TestCategory("Unit")]
public class FixturesDeserializerUnitTests
{
    private List<FixtureMatchModel> _fixtures;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var fixturesDeserializer = new FixturesDeserializer();
        var fileContent = await Helper.ReadFileFromAssetsAsync("get-fixtures-sample-response.json");
        var jsonData = JObject.Parse(fileContent);
        var jsonResultsData = jsonData["response"]![0]!;
        _fixtures = await fixturesDeserializer.Deserialize(jsonResultsData);
    }

    [TestMethod]
    public void AssertEqualFixtureMatchModel_FirstFixture()
    {
        // Arrange
        var firstFixtureMatchModel = _fixtures.First();

        // Act & Assert
        var areEqual = CheckEqual(firstFixtureMatchModel, new FixtureMatchModel
        {
            HomeTeam = new TeamModel { Name = "Burnley" },
            AwayTeam = new TeamModel { Name = "Manchester City" },
            Date = Helper.ConvertDateTimeToString(2023, 8, 11, 19, 0, 0)
        });
        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertEqualFixtureMatchModel_LastFixture()
    {
        var lastFixtureMatchModel = _fixtures.Last();

        var areEqual = CheckEqual(lastFixtureMatchModel, new FixtureMatchModel
        {
            HomeTeam = new TeamModel { Name = "Sheffield Utd" },
            AwayTeam = new TeamModel { Name = "Tottenham" },
            Date = Helper.ConvertDateTimeToString(2024, 5, 19, 15, 0, 0)
        });

        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertNotEqualFixtureMatchModel()
    {
        var fixtureMatchModel1 = new FixtureMatchModel
        {
            HomeTeam = new TeamModel { Name = "InvalidHome" },
            AwayTeam = new TeamModel { Name = "InvalidAway" },
            Date = Helper.ConvertDateTimeToString(2024, 5, 19, 15, 0, 0)
        };

        Assert.IsFalse(CheckEqual(fixtureMatchModel1, _fixtures.First()));
    }

    private bool CheckEqual(FixtureMatchModel expected, FixtureMatchModel actual)
    {
        return expected.HomeTeam.Name == actual.HomeTeam.Name &&
               expected.AwayTeam.Name == actual.AwayTeam.Name &&
               expected.Date == actual.Date;
    }
}