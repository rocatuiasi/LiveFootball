namespace ApiFootballDeserializer;

/// <inheridoc/>
public class DeserializerFactory : IDeserializerFactory
{
    /// <inheridoc/>
    public ILeaguesDeserializer CreateLeaguesDeserializer() => new LeaguesDeserializer();

    /// <inheridoc/>
    public ILiveGamesDeserializer CreateLiveGamesDeserializer() => new LiveGamesDeserializer();

    /// <inheridoc/>
    public IResultsDeserializer CreateResultsDeserializer() => new ResultsDeserializer();

    /// <inheridoc/>
    public IFixturesDeserializer CreateFixturesDeserializer() => new FixturesDeserializer();

    /// <inheridoc/>
    public IStandingDeserializer CreateStandingDeserializer() => new StandingDeserializer();
}