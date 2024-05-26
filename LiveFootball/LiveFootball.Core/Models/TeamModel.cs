/**************************************************************************
 *                                                                        * 
 *  File:        TeamModel.cs                                             *
 *  Description: LiveFootball.Core.Models Library                         *
 *               Represents a sports team, including its logo and name.   *
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
/// Represents a team model containing basic information about a team.
/// </summary>
public class TeamModel
{
    /// <summary>
    /// Gets or sets the logo of the team.
    /// </summary>
    public BitmapSource Logo { get; set; }

    /// <summary>
    /// Gets or sets the name of the team.
    /// </summary>
    public string Name { get; set; }
}