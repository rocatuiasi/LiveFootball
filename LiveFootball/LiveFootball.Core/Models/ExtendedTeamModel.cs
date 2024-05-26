/**************************************************************************
 *                                                                        * 
 *  File:        ExtendedTeamModel.cs                                     *
 *  Description: LiveFootball.Core.Models Library                         *
 *               Represents an extended team model including additional   *
 *               properties such as the number of goals scored.           *
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

public class ExtendedTeamModel : TeamModel
{
    /* public List<string> RedCards { get; set; }
     public List<string> YellowCards { get; set; }*/
    public int Goals { get; set; }
}