
namespace Lands.ViewModels
{
    using Lands.Models;
    using System.Collections.Generic;

    // Este Model es la que controlara el resto de las ViewModels
    public class MainViewModel
    {
        #region Properties
        public List<Land> LandsList { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get; set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }

        public LandViewModel Land
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance==null)
            {
                return new MainViewModel();
            }
            return instance;

        }
        #endregion
    }
}
