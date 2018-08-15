using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Udalosti
{
    public sealed partial class Prihlasenie : Page, KommunikaciaOdpoved
    {
        public Prihlasenie()
        {
            this.InitializeComponent();
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            nacitavanie.IsActive = true;
            nacitavanie.Visibility = Visibility.Visible;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {

                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, prihlasenie);
                    }
                    break;
            }
        }

        private void tlacidloRegistrovatSa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracia), null, new DrillInNavigationTransitionInfo());
        }

        private async void prihlasitAsync(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                await new AutentifkaciaUdaje(this).miestoPrihlaseniaAsync(email.Text, heslo.Password);
            }
            else
            {
                await DialogOznameni.kommunikaciaAsync("Chyba", "Žiadné spojenie!", "Zatvoriť", false, prihlasenie);
            }
        }
    }
}
