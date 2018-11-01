using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Dialog;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Udalosti
{
    public sealed partial class Registracia : Page, KommunikaciaOdpoved
    {
        private AutentifkaciaUdaje autentifkaciaUdaje;

        public Registracia()
        {
            Debug.WriteLine("Metoda Registracia bola vykonana");

            this.InitializeComponent();
            this.init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda init - Registracia bola vykonana");

            this.autentifkaciaUdaje = new AutentifkaciaUdaje(this);

            var uwpSpat = SystemNavigationManager.GetForCurrentView();
            uwpSpat.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private async void registrovat(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda registrovat bola vykonana");

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                await this.autentifkaciaUdaje.registracia(meno.Text, email.Text, heslo.Password, potvrd.Password);
            }
            else
            {
                await DialogOznameni.kommunikaciaAsync("Chyba", "Žiadné spojenie!", "Zatvoriť", false, registracia);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("Metoda OnNavigatedTo - Registracia bola vykonana");

            base.OnNavigatedTo(e);

            SystemNavigationManager.GetForCurrentView().BackRequested += spatNaPrihlasanie;
            registracia.IsEnabled = this.Frame.CanGoBack;
        }

        private void spatNaPrihlasanie(object sender, BackRequestedEventArgs e)
        {
            Debug.WriteLine("Metoda spatNaPrihlasanie bola vykonana");

            if (this.Frame.CanGoBack)
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
        }

        public async Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServeraAsync - Registracia bola vykonana");

            nacitavanie.IsActive = false;
            nacitavanie.Visibility = Visibility.Collapsed;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_REGISTRACIA:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        await DialogOznameni.kommunikaciaAsync("Úspech", "Registrácia prebehla úspesne! Možete sa prihlásiť.", "Ďalej", true, this.Frame);
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, registracia);
                    }
                    break;
            }
        }

        public void odpovedServer(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServera - Registracia bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}