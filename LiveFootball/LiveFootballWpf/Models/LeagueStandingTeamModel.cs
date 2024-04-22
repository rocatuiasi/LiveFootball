namespace LiveFootballWpf.Models
{
    public class LeagueStandingTeamModel
    {
        #region Backing Fields and Properties

        private int _position;

		public int Position
		{
			get => _position;
            set => _position = value;
        }

        private string _club;

        public string Club
        {
            get => _club;
            set => _club = value;
        }

        private int _matchesPlayed;

        public int MatchesPlayed
        {
            get => _matchesPlayed;
            set => _matchesPlayed = value;
        }

        private int _matchesWon;

        public int MatchesWon
        {
            get => _matchesWon;
            set => _matchesWon = value;
        }

        private int _matchesDrawn;

        public int MatchesDrawn
        {
            get => _matchesDrawn;
            set => _matchesDrawn = value;
        }

        private int _matchesLost;

        public int MatchesLost
        {
            get => _matchesLost;
            set => _matchesLost = value;
        }

        private int _goalsFor;

        public int GoalsFor
        {
            get => _goalsFor;
            set => _goalsFor = value;
        }

        private int _goalsAgainst;

        public int GoalsAgainst
        {
            get => _goalsAgainst;
            set => _goalsAgainst = value;
        }

        private int _goalDifference;

        public int GoalDifference
        {
            get => _goalDifference;
            set => _goalDifference = value;
        }

        private int _points;

        public int Points
        {
            get => _points;
            set => _points = value;
        }

        private string _form;

        public string Form
        {
            get => _form;
            set => _form = value;
        }

        #endregion

        #region Constructors

        public LeagueStandingTeamModel()
        {
            _position = 0;
            _club = string.Empty;
            _matchesPlayed = 0;
            _matchesWon = 0;
            _matchesDrawn = 0;
            _matchesLost = 0;
            _goalsFor = 0;
            _goalsAgainst = 0;
            _goalDifference = 0;
            _points = 0;
            _form = string.Empty;
        }

        public LeagueStandingTeamModel(int position, string club, int matchesPlayed, int matchesWon, int matchesDrawn,
                                       int matchesLost, int goalsFor, int goalsAgainst, int goalDifference,
                                       int points, string form)
        {
            _position = position;
            _club = club;
            _matchesPlayed = matchesPlayed;
            _matchesWon = matchesWon;
            _matchesDrawn = matchesDrawn;
            _matchesLost = matchesLost;
            _goalsFor = goalsFor;
            _goalsAgainst = goalsAgainst;
            _goalDifference = goalDifference;
            _points = points;
            _form = form;
        }

        #endregion
    }
}
