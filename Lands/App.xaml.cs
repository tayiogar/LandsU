
namespace Lands
{
    // Recomenacion meter los Using dentro del namespace es mas facil copiar y pegar
    using Xamarin.Forms;
    using Lands.Views;

    public partial class App : Application
    {
        #region Constructors
        public App()
        {
            InitializeComponent();
            // Al agregar NavigationPage agrega una rayita en la parte de arriba de el layout
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
