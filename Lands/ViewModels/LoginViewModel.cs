namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using System.ComponentModel;
    using Xamarin.Forms;
    using Views;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;


    public class LoginViewModel : BaseViewModel
    {

        /* creamos una propiedad privada por cada atributo que queremos refrescar
        aqui se colocan con minusculas 
        luego debemos camviar en las propiedades en el GET y SET*/
        #region Attrubutes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion



        #region Properties
        public string Email
        {
            // Esto garantiza que se refresque el email
            get
            {
                return this.email;
            }
            set
            {
                SetValue(ref this.email, value);
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                //Antes
                //if (this.password != value)
                //{
                //    this.password = value;
                //    // para refrescar el atributo 
                //    PropertyChanged?.Invoke(
                //        this,
                //        new PropertyChangedEventArgs(nameof(this.Password)));
                //}

                // Ahora
                SetValue(ref this.password, value);

            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                SetValue(ref this.isEnabled, value);
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            /* como los bool al iniciar el aplicativo inician como False en el contsructor debemos 
            colocarle True para activarlo*/
            this.IsEnabled = true;

            // Temporalmente vamos a quemar el mail y el pasword
            this.Email = "tayiogar@gmail.com";
            this.Password = "Tayio";

            //https://restcountries.eu/rest/v2/all

        }
        #endregion

        #region Commands
        /* 
        Agregamos NuGet - Solution MvvmLightLibs
        Como es un comando de solo lectura usamos GET.
        
        Recomendación si el comando se llama LoginCommand le quitamos el apellido Command osea Login
        */
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            /* Validar si se realizo login y password .
            
            string.IsNullOrEmpty(this.Email) = valida que el campo es null o limpio
            el metodo async hay que colocarle await ya que sigue para abajo y este lo ataja o toma
            
            Xamarin.Forms.Application.Current.MainPage.DisplayAlert = es para crear un mensaje
            
            */
            if (string.IsNullOrEmpty(this.Email))
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                "Error",
                "You must enter an email.",
                "Accept");

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                "Error",
                "You must enter a Password.",
                "Accept");

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            // valida los datos
            if (this.Email != "tayiogar@gmail.com" || this.Password != "Tayio")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                "Error",
                "Email or Password incorrect.",
                "Accept");

                /* para limpiear el campo si no esta bien los valores usamos 
                string.Empty */
                this.Password = string.Empty;

                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;
            // Aqui estamos limpiando los campos para que se limpien cuando entra a la otra form y debemos ponerla a regrescar 
            this.Email = string.Empty;
            this.password = string.Empty;

            MainViewModel.GetInstance().Lands = new LandsViewModel();
            // Aqui estamos apilando otra pag ina.
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }
        #endregion
    }
}
