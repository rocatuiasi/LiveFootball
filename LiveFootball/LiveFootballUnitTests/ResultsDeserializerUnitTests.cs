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
[TestSubject(typeof(ResultsDeserializer))]
[TestCategory("Unit")]
public class ResultsDeserializerUnitTests
{
    private List<ResultMatchModel> _results;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var resultsDeserializer = new ResultsDeserializer();
        var fileContent = await Helper.ReadFileFromAssetsAsync("get-results-sample-response.json");
        var jsonData = JObject.Parse(fileContent);
        var jsonResultsData = jsonData["response"]![0]!;
        _results = await resultsDeserializer.Deserialize(jsonResultsData);
    }

    [TestMethod]
    public void AssertEqualResultMatchModel_FirstResult()
    {
        // Arrange
        var firstResultMatchModel = _results.First();

        // Act & Assert
        var areEqual = CheckEqual(firstResultMatchModel, new ResultMatchModel
        {
            HomeTeam = new ExtendedTeamModel { Name = "Manchester City", Goals = 3 },
            AwayTeam = new ExtendedTeamModel { Name = "West Ham", Goals = 1 },
            Date = Helper.ConvertDateTimeToString(2024, 5, 19, 15, 0, 0)
        });
        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertEqualResultMatchModel_LastResult()
    {
        var lastResultMatchModel = _results.Last();

        var areEqual = CheckEqual(lastResultMatchModel, new ResultMatchModel
        {
            HomeTeam = new ExtendedTeamModel { Name = "Burnley", Goals = 1 },
            AwayTeam = new ExtendedTeamModel { Name = "Newcastle", Goals = 4 },
            Date = Helper.ConvertDateTimeToString(2024, 5, 4, 14, 0, 0)
        });
        
        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertNotEqualResultMatchModel()
    {
        var matchModel1 = new ResultMatchModel
        {
            HomeTeam = new ExtendedTeamModel { Name = "InvalidHome", Goals = 99 },
            AwayTeam = new ExtendedTeamModel { Name = "InvalidAway", Goals = 1 },
            Date = Helper.ConvertDateTimeToString(2024, 5, 19, 15, 0, 0)
        };
        
        Assert.IsFalse(CheckEqual(matchModel1, _results.First()));
    }

    private bool CheckEqual(ResultMatchModel expected, ResultMatchModel actual)
    {
        return expected.HomeTeam.Name == actual.HomeTeam.Name &&
               expected.AwayTeam.Name == actual.AwayTeam.Name &&
               expected.HomeTeam.Goals == actual.HomeTeam.Goals &&
               expected.AwayTeam.Goals == actual.AwayTeam.Goals &&
               expected.Date == actual.Date;
    }
}
