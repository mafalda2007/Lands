namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;
    

    public class LoginViewModel : BaseViewModel 
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            // Esto refresca el email. 
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public bool IsRemembered { get;set; }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion
        
        #region Contructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemembered = true;
            this.IsEnabled = true;
            // Para no escribir tanto en las pruebas
            /*this.Email = "jcpell@gmail.com";
            this.Password = "1234";*/
        }
        #endregion

        #region Command
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }


        private async void Login()
        {
            if(string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Ud. debe ingresar un email", "Aceptar");
                return;
            }
            if(string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Ud. debe ingresar un password","Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;


            /*if(this.Email !="jcpell@gmail.com" || this.Password !="1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Email o Password incorrecto", "Aceptar");
                this.Password = string.Empty;
                return;
            }*/
           
            var connection = await this.apiService.CheckConnection();
            if(!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    connection.Message, 
                    "Aceptar");
                return;
            }
            var token = await this.apiService.GetToken(
                "http://usuarios.crediguia.com.ar:44548",
                this.Email,
                this.Password);

            if (token == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error", "Algo salió mal, por favor intente más tarde", "Aceptar");
                return;
            }

            if(string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    token.ErrorDescription, 
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

       
        }
        #endregion
    }
}
