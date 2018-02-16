
namespace Lands.ViewModels
{

    using System;
    // Esta es la clase principal y la que gobierna a todas las del proyecto
    using System.Diagnostics.Contracts;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region ViewModels
        // se referncia la Login
        public LoginViewModel Login
        {
            get;
            set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        // por ser la pagina principal debemos instanciarla
        // ctor tab+tab contruye en constructor
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            // al poner this decimos que es una propiedad de la clase es obligatorio
        }
        #endregion

        #region Singleton
        // nos permite hacer un llamado de la MainViewModel en cualquier lado de la aplicacion
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }


        #endregion

    }
}
