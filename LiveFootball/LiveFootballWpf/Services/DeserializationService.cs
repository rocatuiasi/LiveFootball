/**************************************************************************
 *                                                                        * 
 *  File:        DeserializationService.cs                                *
 *  Description: LiveFootballWpf.Services Library                         *
 *               Service for deserializing JSON data from API responses   *
 *               into application models.                                 *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using ApiFootballDeserializer;
using LiveFootball.Core.Exceptions;
using LiveFootball.Core.Models;
using LiveFootball.Core.Services;
using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace LiveFootballWpf.Services;

public class DeserializationService : IDeserializationService
{
    private readonly IDeserializerFactory _deserializerFactory;

    public DeserializationService(IDeserializerFactory deserializerFactory)
    {
        _deserializerFactory = deserializerFactory;
    }

    public async Task<List<LiveMatchModel>> DeserializeLiveGamesData(JObject jsonData)
    {
        try
        {
            var jsonLiveGamesData = jsonData["response"]![0]!;

            return await _deserializerFactory.CreateLiveGamesDeserializer().Deserialize(jsonLiveGamesData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<ResultMatchModel>> DeserializeResultsData(JObject jsonData)
    {
        try
        {
            var jsonResultsData = jsonData["response"]![0]!;

            return await _deserializerFactory.CreateResultsDeserializer().Deserialize(jsonResultsData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<FixtureMatchModel>> DeserializeFixturesData(JObject jsonData)
    {
        try
        {
            var jsonFixtureData = jsonData["response"]![0]!;

            return await _deserializerFactory.CreateFixturesDeserializer().Deserialize(jsonFixtureData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<LeagueStandingTeamModel>> DeserializeStandingData(JObject jsonData)
    {
        try
        {
            var jsonStandingData = jsonData["response"]![0]!["league"]!["standings"]![0]![0]!;

            return await _deserializerFactory.CreateStandingDeserializer().Deserialize(jsonStandingData);
        }
        catch (Exception)
        {
            throw new DeserializationException();
        }
    }

    public async Task<List<MenuItemModel>> DeserializeLeaguesData(JObject jsonData)
    {
        // var jsonLeaguesData = jsonData["response"]![0]!;
        
        return await _deserializerFactory.CreateLeaguesDeserializer().Deserialize(jsonData);
    }
}