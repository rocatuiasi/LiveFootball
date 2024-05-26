/**************************************************************************
 *                                                                        * 
 *  File:        ResultMatchModel.cs                                      *
 *  Description: LiveFootball.Core.Models Library                         *
 *               Represents a result match, including match date and      *
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
/// Represents a result match model containing information about a completed match.
/// </summary>
public class ResultMatchModel
{
    /// <summary>
    /// Gets or sets the date and time of the match.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Gets or sets the home team participating in the match.
    /// </summary>
    public ExtendedTeamModel HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the away team participating in the match.
    /// </summary>
    public ExtendedTeamModel AwayTeam { get; set; }
}