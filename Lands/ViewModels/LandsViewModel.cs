using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Linq;
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Lands.Services;
    using Models;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    public class LandsViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Land> lands;
        private bool isRefreshing;
        // Aqui agregamos el search
        private string filter;
        private List<Land> landsList;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                // con esto hace la busqueda por letra o quitamos sino lo deseamos buscar
                this.Search();
            }
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
            //this.IsRefreshing = true;

            //// Antes de mandar una peticion verfificamos si existe coneccion en internet

            //var connection = await this.apiService.CheckConnection();

            //if (!connection.IsSuccess)
            //{
            //    this.IsRefreshing = false;
            //    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
            //        "Error"
            //        , connection.Message
            //        , "Accept");

            //    /*
            //    PopAsync
            //    Si no existe coneccion puedo sacarlo al login nuevamente
            //    Tiene la misma funcionalidad que del back
            //    */

            //    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

            //    return;
            //}


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
                //  await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();


                return;
            }

            // this.landsList lo hacemos para mantener en memoria el listado original
            this.landsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(this.landsList);
            this.IsRefreshing = false;

        }
        #endregion

        #region Commands

        public ICommand RefRefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        // Command de Search
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        // Creamos el metodo del Search
        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList);
            }
            else
            {
                // Estamos realizando la busqueda por name y capital
                this.Lands = new ObservableCollection<Land>(
                    this.landsList.Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }



        #endregion
    }
}
