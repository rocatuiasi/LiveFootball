namespace ApiFootballDeserializer;

public class DeserializerFactory : IDeserializerFactory
{
    public ILiveGamesDeserializer CreateLiveGamesDeserializer() => new LiveGamesDeserializer();

    public IResultsDeserializer CreateResultsDeserializer() => new ResultsDeserializer();

    public IFixturesDeserializer CreateFixturesDeserializer() => new FixturesDeserializer();

    public IStandingDeserializer CreateStandingDeserializer() => new StandingDeserializer();
    
    public ILeaguesDeserializer CreateLeaguesDeserializer() => new LeaguesDeserializer();
}