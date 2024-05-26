/**************************************************************************
 *                                                                        * 
 *  File:        FixtureMatchModel.cs                                     *
 *  Description: LiveFootball Core Library                                *
 *               Represents a fixture match model containing information  *
 *               about a scheduled match.                                 *
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
/// Represents a fixture match model containing information about a scheduled match.
/// </summary>
public class FixtureMatchModel
{
    /// <summary>
    /// Gets or sets the date and time of the match.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Gets or sets the home team participating in the match.
    /// </summary>
    public TeamModel HomeTeam { get; set; }

    /// <summary>
    /// Gets or sets the away team participating in the match.
    /// </summary>
    public TeamModel AwayTeam { get; set; }
}