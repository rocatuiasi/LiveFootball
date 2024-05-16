using LiveFootball.Core.Models;
using Newtonsoft.Json.Linq;

namespace LiveFootball.Core.Deserializers;

public class ResultsDeserializer : IResultsDeserializer
{
    public Task<List<ExtendedMatchModel>> Deserialize(JToken jsonData)
    {
        throw new NotImplementedException();
    }
}