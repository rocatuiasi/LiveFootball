using LiveFootball.Core.Models;
using LiveFootball.Core.ViewModels;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Services;

public interface IDeserializeResponseDataService
{
    List<LeagueStandingTeamModel> DeserializeStandingData(JObject jsonData);
}