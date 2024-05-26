/**************************************************************************
 *                                                                        *
 *  File:        DeserializerFactory.cs                                   *
 *  Description: ApiFootballDeserializer Library                          *
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
/// Factory class for creating deserializers for various football data types.
/// </summary>
public class DeserializerFactory : IDeserializerFactory
{
    /// <inheritdoc/>
    public ILeaguesDeserializer CreateLeaguesDeserializer() => new LeaguesDeserializer();

    /// <inheritdoc/>
    public ILiveGamesDeserializer CreateLiveGamesDeserializer() => new LiveGamesDeserializer();

    /// <inheritdoc/>
    public IResultsDeserializer CreateResultsDeserializer() => new ResultsDeserializer();

    /// <inheritdoc/>
    public IFixturesDeserializer CreateFixturesDeserializer() => new FixturesDeserializer();

    /// <inheritdoc/>
    public IStandingDeserializer CreateStandingDeserializer() => new StandingDeserializer();
}