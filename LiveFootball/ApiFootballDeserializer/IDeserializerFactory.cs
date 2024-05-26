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

public interface IDeserializerFactory
{
    ILiveGamesDeserializer CreateLiveGamesDeserializer();

    IResultsDeserializer CreateResultsDeserializer();

    IFixturesDeserializer CreateFixturesDeserializer();

    IStandingDeserializer CreateStandingDeserializer();

    ILeaguesDeserializer CreateLeaguesDeserializer();
}