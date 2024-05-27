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
[TestSubject(typeof(LeaguesDeserializer))]
[TestCategory("Unit")]
public class LeaguesDeserializerUnitTests
{
    private List<MenuItemModel> _leauges;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var leaguesDeserializer = new LeaguesDeserializer();
        var fileContent = await Helper.ReadFileFromAssetsAsync("get-leagues-sample-response.json");
        var jsonData = JObject.Parse(fileContent);
        _leauges = await leaguesDeserializer.Deserialize(jsonData);
    }

    [TestMethod]
    public void AssertEqualFixtureMatchModel_FirstFixture()
    {
        // Arrange
        var firstLeague = _leauges.First();

        // Act & Assert
        var areEqual = CheckEqual(firstLeague, new MenuItemModel("Euro Championship", "4", null));
        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertEqualLiveMatchModel_LastFixture()
    {
        var lastLeague = _leauges.Last();

        var areEqual = CheckEqual(lastLeague, new MenuItemModel("UEFA Championship - Women - Qualification", "1083", null));

        Assert.IsTrue(areEqual);
    }

    [TestMethod]
    public void AssertNotEqualLiveMatchModel()
    {
        var league = new MenuItemModel("InvalidLeague", "3", null);

        Assert.IsFalse(CheckEqual(league, _leauges.First()));
    }

    private bool CheckEqual(MenuItemModel expected, MenuItemModel actual)
    {
        return expected.Name == actual.Name &&
               expected.LeagueId == actual.LeagueId;
    }
}