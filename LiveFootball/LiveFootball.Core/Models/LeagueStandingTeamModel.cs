/**************************************************************************
 *                                                                        * 
 *  File:        LeagueStandingTeamModel.cs                               *
 *  Description: LiveFootball Core Library                                *
 *               Represents a team's standing in a league, including      *
 *               various statistical information.                         *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System.Windows.Media.Imaging;

namespace LiveFootball.Core.Models;

/// <summary>
/// Represents a team's standing in a league, including various statistical information.
/// </summary>
public class LeagueStandingTeamModel
{
    #region Properties

    /// <summary>
    /// Gets the position of the team in the league standings.
    /// </summary>
    public int Position { get; init; }

    /// <summary>
    /// Gets the logo of the team.
    /// </summary>
    public BitmapSource Logo { get; init; }

    /// <summary>
    /// Gets the name of the club.
    /// </summary>
    public string Club { get; init; }

    /// <summary>
    /// Gets the number of matches played by the team.
    /// </summary>
    public int MatchesPlayed { get; init; }

    /// <summary>
    /// Gets the number of matches won by the team.
    /// </summary>
    public int MatchesWon { get; init; }

    /// <summary>
    /// Gets the number of matches drawn by the team.
    /// </summary>
    public int MatchesDrawn { get; init; }

    /// <summary>
    /// Gets the number of matches lost by the team.
    /// </summary>
    public int MatchesLost { get; init; }

    /// <summary>
    /// Gets the number of goals scored by the team.
    /// </summary>
    public int GoalsFor { get; init; }

    /// <summary>
    /// Gets the number of goals conceded by the team.
    /// </summary>
    public int GoalsAgainst { get; init; }

    /// <summary>
    /// Gets the goal difference of the team.
    /// </summary>
    public int GoalDifference { get; init; }

    /// <summary>
    /// Gets the total points earned by the team.
    /// </summary>
    public int Points { get; init; }

    /// <summary>
    /// Gets the recent form of the team.
    /// </summary>
    public string Form { get; init; }

    #endregion
}