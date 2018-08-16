using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udalosti.UI;
using Udalosti.Uvod.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Udalosti
{
    public sealed partial class Prihlasenie : Page, KommunikaciaOdpoved
    {
        private AutentifkaciaUdaje autentifkaciaUdaje;

        public Prihlasenie()
        {
            this.InitializeComponent();
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
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(email.Text, heslo.Password);
                        prihlasenie.Navigate(typeof(ZoznamUdalosti), null, new DrillInNavigationTransitionInfo());
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
            this.Frame.Navigate(typeof(Registracia));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string chyba = (string)e.Parameter;
            if (!string.IsNullOrWhiteSpace(chyba) && e.Parameter != null && e.NavigationMode == NavigationMode.New)
            {
                if (chyba.Equals("neUspesnePrihlasenie"))
                {
                    await DialogOznameni.kommunikaciaAsync("Chyba", "Prosím prihláste sa!", "Zatvoriť", false, prihlasenie);
                    this.autentifkaciaUdaje.ucetJeNePristupny(new UvodnaObrazovkaUdaje().prihlasPouzivatela()["email"]);
                }
                else if (chyba.Equals("chyba"))
                {
                    await DialogOznameni.kommunikaciaAsync("Chyba", "Nastala chyba, prosím prihláste sa!", "Zatvoriť", false, prihlasenie);
                }
            }
            this.Frame.BackStack.Clear();
        }

        private async void prihlasitAsync(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                await autentifkaciaUdaje.miestoPrihlaseniaAsync(email.Text, heslo.Password);
            }
            else
            {
                await DialogOznameni.kommunikaciaAsync("Chyba", "Žiadné spojenie!", "Zatvoriť", false, prihlasenie);
            }
        }
    }
}