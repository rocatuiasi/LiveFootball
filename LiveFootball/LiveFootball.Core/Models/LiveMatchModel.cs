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

/// <summary>
/// Represents a live match with status and participating teams' information.
/// </summary>
public class LiveMatchModel
{
    /// <summary>
    /// Gets or sets the status of the match (e.g., live, half-time, full-time).
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the home team participating in the match.
    /// </summary>
    public ExtendedTeamModel HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the away team participating in the match.
    /// </summary>
    public ExtendedTeamModel AwayTeam { get; set; }
}