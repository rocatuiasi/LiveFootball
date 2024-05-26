namespace ApiFootballDeserializer;

/// <summary>
/// Interface for a factory that creates deserializers for various football data types.
/// </summary>
public interface IDeserializerFactory
{
    /// <summary>
    /// Creates an instance of a deserializer for leagues.
    /// </summary>
    /// <returns>An object implementing ILeaguesDeserializer.</returns>
    ILeaguesDeserializer CreateLeaguesDeserializer();

    /// <summary>
    /// Creates an instance of a deserializer for live games.
    /// </summary>
    /// <returns>An object implementing ILiveGamesDeserializer.</returns>
    ILiveGamesDeserializer CreateLiveGamesDeserializer();

    /// <summary>
    /// Creates an instance of a deserializer for results.
    /// </summary>
    /// <returns>An object implementing IResultsDeserializer.</returns>
    IResultsDeserializer CreateResultsDeserializer();

    /// <summary>
    /// Creates an instance of a deserializer for fixtures.
    /// </summary>
    /// <returns>An object implementing IFixturesDeserializer.</returns>
    IFixturesDeserializer CreateFixturesDeserializer();

    /// <summary>
    /// Creates an instance of a deserializer for standings.
    /// </summary>
    /// <returns>An object implementing IStandingDeserializer.</returns>
    IStandingDeserializer CreateStandingDeserializer();
}