using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udalosti.UI;
using Udalosti.Uvod.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml;
using System.Diagnostics;

namespace Udalosti.Uvod.UI
{
    public sealed partial class UvodnaObrazovka : Page, KommunikaciaOdpoved
    {
        private AutentifkaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        public UvodnaObrazovka()
        {
            this.InitializeComponent();

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.autentifkaciaUdaje = new AutentifkaciaUdaje(this);
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            nacitavanie.IsActive = false;
            nacitavanie.Visibility = Visibility.Collapsed;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        uvodnaObrazovka.Navigate(typeof(ZoznamUdalosti), new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(Prihlasenie), "neUspesnePrihlasenie");
                    }
                    break;
            }
        }

        private async void automatickePrihlasenie(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda automatickePrihlasenie bola vykonana");
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                Dictionary<string, string> pouzivatelskeUdaje = uvodnaObrazovkaUdaje.prihlasPouzivatela();
                await autentifkaciaUdaje.miestoPrihlaseniaAsync(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["heslo"]);
            }
            else
            {
                this.Frame.Navigate(typeof(Prihlasenie), "chyba");
            }
        }
    }
}