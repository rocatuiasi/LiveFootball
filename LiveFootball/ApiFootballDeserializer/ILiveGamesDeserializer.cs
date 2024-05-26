/**************************************************************************
 *                                                                        * 
 *  File:        ILiveGamesDeserializer.cs                                *
 *  Description: ApiFootballDeserializer Library                          *
 *               Interface for deserializing live game data               *
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

/// <summary>
/// Interface for deserializing live game data from JSON.
/// </summary>
public interface ILiveGamesDeserializer
{
    /// <summary>
    /// Deserializes the given JSON token into a list of live match models.
    /// </summary>
    /// <param name="jsonData">The JSON data to deserialize.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="LiveMatchModel"/> as the result.</returns>
    public Task<List<LiveMatchModel>> Deserialize(JToken jsonData);
}