namespace ApiFootballDeserializer;

public interface IDeserializerFactory
{
    ILiveGamesDeserializer CreateLiveGamesDeserializer();

    IResultsDeserializer CreateResultsDeserializer();

    IFixturesDeserializer CreateFixturesDeserializer();

    IStandingDeserializer CreateStandingDeserializer();

    ILeaguesDeserializer CreateLeaguesDeserializer();
}