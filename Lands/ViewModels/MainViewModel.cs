using System;
// Esta es la clase principal y la que gobierna a todas las del proyecto
using System.Diagnostics.Contracts;
using Xamarin.Forms;
namespace Lands.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        // se referncia la Login
        public LoginViewModel Login
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
            this.Login = new LoginViewModel();
            // al poner this decimos que es una propiedad de la clase es obligatorio
        }
        #endregion

    }
}
