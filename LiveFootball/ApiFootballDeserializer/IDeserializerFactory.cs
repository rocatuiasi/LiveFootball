/**************************************************************************
 *                                                                        * 
 *  File:        IDeserializerFactory.cs                                  *
 *  Description: ApiFootballDeserializer Library                          *
 *               Interface for creating various deserializers             *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
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