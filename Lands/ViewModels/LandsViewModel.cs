using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Lands.Services;
    using Models;

    public class LandsViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private ObservableCollection<Land> lands;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region methods 
        private async void LoadLands()
        {

            // Antes de mandar una peticion verfificamos si existe coneccion en internet

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error"
                    , connection.Message
                    , "Accept");

                //Si no existe coneccion puedo sacarlo al login nuevamente

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

                return;
            }


            // Aqui mandamos la peticion para consumir los paises
            var response = await this.apiService.GetList<Land>(
            "https://restcountries.eu",
            "/rest",
            "/v2/all");

            //Validamos que si se pudo hacer la consulta
            if (!response.IsSuccess)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error"
                    , response.Message
                    , "Accept");

                //Aqui tambien envia sino exista coneccion enviara al LOGIN
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();


                return;
            }

            var list = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(list);
        }
        #endregion
    }
}
