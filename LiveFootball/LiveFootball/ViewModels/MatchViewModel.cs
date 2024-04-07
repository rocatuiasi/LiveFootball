using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveFootball.ViewModels
{
    public class MatchViewModel : ViewModelBase
    {
        public string Time { get; set; }
        public TeamViewModel HomeTeam { get; set; }
        public TeamViewModel AwayTeam { get; set; }
    }
}
