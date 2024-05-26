/**************************************************************************
 *                                                                        * 
 *  File:        IStandingDeserializer.cs                                 *
 *  Description: ApiFootballDeserializer Library                          *
 *               Interface for deserializing league standing data         *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface IStandingDeserializer
{
    public Task<List<LeagueStandingTeamModel>> Deserialize(JToken jsonData);
}