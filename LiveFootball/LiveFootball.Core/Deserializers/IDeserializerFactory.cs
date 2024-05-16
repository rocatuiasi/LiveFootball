namespace LiveFootball.Core.Deserializers;

public interface IDeserializerFactory
{
    IStandingDeserializer CreateStandingDeserializer();
    IFixturesDeserializer CreateFixturesDeserializer();
    IResultsDeserializer CreateResultsDeserializer();
}