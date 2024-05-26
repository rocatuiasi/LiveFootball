using System;
using System.Linq;
using ApiFootballDeserializer;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiveFootballUnitTests;

[TestClass]
[TestSubject(typeof(FixturesDeserializer))]
[TestCategory("Unit")]
public class FixturesDeserializerUnitTests
{
    [TestMethod]
    public async void Deserialize_ValidJson_ReturnsFixtureObject()
    {
        // Arrange
        var json = "{\"id\": 1, \"homeTeam\": \"Team A\", \"awayTeam\": \"Team B\", \"date\": \"2022-01-01\"}";
        var deserializer = new FixturesDeserializer();

        // Act
        var fixture = (await deserializer.Deserialize(json)).First();

        // Assert
        Assert.IsNotNull(fixture);
        // Assert.AreEqual(1, fixture.);
        Assert.AreEqual("Team A", fixture.HomeTeam.Name);
        Assert.AreEqual("Team B", fixture.AwayTeam.Name);
        // Assert.AreEqual(new DateTime(2022, 01, 01), fixture.Date);
    }

    [TestMethod]
    public void Deserialize_InvalidJson_ThrowsException()
    {
        // Arrange
        var json = "{\"id\": 1, \"homeTeam\": \"Team A\", \"awayTeam\": \"Team B\"}";
        var deserializer = new FixturesDeserializer();

        // Act & Assert
        Assert.ThrowsException<Exception>(() => deserializer.Deserialize(json));
    }
}