using System.Diagnostics;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class Podrobnosti : Page
    {
        public Podrobnosti()
        {
            this.InitializeComponent();
            this.init();
        }

        private void init()
        {
            var uwpSpat = SystemNavigationManager.GetForCurrentView();
            uwpSpat.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var aktualnaUdalost = (Udalost)e.Parameter;

            SystemNavigationManager.GetForCurrentView().BackRequested += spatnNaZoznamUdalosti;
            podrobnosti.IsEnabled = this.Frame.CanGoBack;
        }

        private void spatnNaZoznamUdalosti(object sender, BackRequestedEventArgs e)
        {
            Debug.WriteLine("Metoda spatnNaZoznamUdalosti bola vykonana");

            if (this.Frame.CanGoBack)
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
        }
    }
}
