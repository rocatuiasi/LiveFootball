using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public interface IResultsDeserializer
{
    public Task<List<ExtendedMatchModel>> Deserialize(JToken jsonData);
}