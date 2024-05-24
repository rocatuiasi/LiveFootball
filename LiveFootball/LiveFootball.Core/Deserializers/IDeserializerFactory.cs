namespace LiveFootball.Core.Deserializers;

public interface IDeserializerFactory
{
    ILiveGamesDeserializer CreateLiveGamesDeserializer();

    IResultsDeserializer CreateResultsDeserializer();

    IFixturesDeserializer CreateFixturesDeserializer();

    IStandingDeserializer CreateStandingDeserializer();

    ILeaguesDeserializer CreateLeaguesDeserializer();
}