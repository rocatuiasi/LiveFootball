using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace ApiFootballDeserializer;

public interface IResultsDeserializer
{
    public Task<List<ResultMatchModel>> Deserialize(JToken jsonData);
}