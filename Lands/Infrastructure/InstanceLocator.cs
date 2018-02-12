namespace Lands.Infrastructure

{

    using ViewModels;

    public class InstanceLocator
    {

        #region Properties
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        // Crear constructores CTOR
        // poder ligar la pagina login a viewModel
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
