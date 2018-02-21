
namespace Lands.ViewModels
{
    using System;
    using Lands.Models;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Lands.Views;

    public class LandItemViewModel : Land
    {
        #region Commands
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        private async void SelectLand()
        {
            /*
                Debemos antes de pintar la ViewModel que debemos instanciar  la viewModel a la Page
            */
            MainViewModel.GetInstance().Land = new LandViewModel(this);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LandPage());
        }
        #endregion
    }
}
