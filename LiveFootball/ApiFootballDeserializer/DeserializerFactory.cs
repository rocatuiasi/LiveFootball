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

public class DeserializerFactory : IDeserializerFactory
{
    public ILiveGamesDeserializer CreateLiveGamesDeserializer() => new LiveGamesDeserializer();

    public IResultsDeserializer CreateResultsDeserializer() => new ResultsDeserializer();

    public IFixturesDeserializer CreateFixturesDeserializer() => new FixturesDeserializer();

    public IStandingDeserializer CreateStandingDeserializer() => new StandingDeserializer();
    
    public ILeaguesDeserializer CreateLeaguesDeserializer() => new LeaguesDeserializer();
}