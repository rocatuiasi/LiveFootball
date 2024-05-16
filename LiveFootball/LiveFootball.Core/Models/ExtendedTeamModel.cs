namespace LiveFootball.Core.Models;

public class ExtendedTeamModel : TeamModel
{
    public List<string> RedCards { get; set; }
    public List<string> YellowCards { get; set; }
    public string Goals { get; set; }
}