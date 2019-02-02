namespace Lands
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Views;

    public partial class App : Application
    {
        #region Contructor
        public App()
        {
            InitializeComponent();

            this.MainPage = new NavigationPage(new LoginPage());
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
