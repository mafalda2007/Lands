
namespace Lands.Infrastructure
{
    using ViewModels;


    class InstanceLocator
    {
        #region Properties
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion
        // Sirve para ligar la pag login a la mainviewmodel 
        #region Constructor
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
