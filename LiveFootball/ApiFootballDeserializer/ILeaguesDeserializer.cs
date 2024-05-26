/**************************************************************************
 *                                                                        * 
 *  File:        ILeaguesDeserializer.cs                                  *
 *  Description: ApiFootballDeserializer Library                          *
 *               Interface for deserializing league data                  *
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
using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface ILeaguesDeserializer
{
    public Task<List<MenuItemModel>> Deserialize(JToken jsonData);
}