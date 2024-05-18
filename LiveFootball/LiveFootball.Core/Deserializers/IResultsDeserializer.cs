using LiveFootball.Core.Models;

using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public interface IResultsDeserializer
{
    public Task<List<ResultMatchModel>> Deserialize(JToken jsonData);
}