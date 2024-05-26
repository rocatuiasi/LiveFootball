/**************************************************************************
 *                                                                        * 
 *  File:        LiveMatchModel.cs                                        *
 *  Description: LiveFootball.Core.Models Library                         *
 *               Represents a live match, including match status and      *
 *               information about the home and away teams.               *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
namespace LiveFootball.Core.Models;

public class LiveMatchModel
{
    public string Status { get; set; }
    public ExtendedTeamModel HomeTeam { get; set; }
    public ExtendedTeamModel AwayTeam { get; set; }
}