namespace LiveFootball.Core.Deserializers;

public class DeserializerFactory : IDeserializerFactory
{
    public IStandingDeserializer CreateStandingDeserializer() => new StandingDeserializer();

    public IFixturesDeserializer CreateFixturesDeserializer() => new FixturesDeserializer();

    public IResultsDeserializer CreateResultsDeserializer() => new ResultsDeserializer();
}