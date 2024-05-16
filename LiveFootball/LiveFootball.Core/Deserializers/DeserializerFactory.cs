namespace LiveFootball.Core.Deserializers;

public class DeserializerFactory : IDeserializerFactory
{
    public IStandingDeserializer CreateStandingDeserializer()
    {
        return new StandingDeserializer();
    }

    public IFixturesDeserializer CreateFixturesDeserializer()
    {
        return new FixturesDeserializer();
    }

    public IResultsDeserializer CreateResultsDeserializer()
    {
        return new ResultsDeserializer();
    }
}