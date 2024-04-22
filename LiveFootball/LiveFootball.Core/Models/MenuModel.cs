namespace LiveFootball.Core.Models
{
    public class MenuModel
    {
        #region Backing Fields and Properties

        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion

        #region Constructors

        public MenuModel() 
        { 
            _name = string.Empty;
        }

        public MenuModel(string name)
        {
            _name = name;
        }

        #endregion
    }
}
