using System;
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
using Windows.UI.Core;
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
            this.init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda init - Prihlasenie bola vykonana");

            this.autentifkaciaUdaje = new AutentifkaciaUdaje(this);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private async void prihlasitAsync(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda prihlasitAsync bola vykonana");

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                Dictionary<string, double> poloha = await Lokalizator.zistiPolohuAsync();
                if (poloha == null)
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(email.Text, heslo.Password);
                }
                else
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(email.Text, heslo.Password, poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], false);
                }
            }
            else
            {
                await DialogOznameni.kommunikaciaAsync("Chyba", "Žiadné spojenie!", "Zatvoriť", false, prihlasenie);
            }
        }

        private void tlacidloRegistrovatSa(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda tlacidloRegistrovatSa bola vykonana");

            this.Frame.Navigate(typeof(Registracia));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("Metoda OnNavigatedTo - Prihlasenie bola vykonana");

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
                this.Frame.BackStack.Clear();
            }
        }

        public async Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServera - Prihlasenie bola vykonana");

            nacitavanie.IsActive = false;
            nacitavanie.Visibility = Visibility.Collapsed;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(email.Text, heslo.Password, udaje["token"]);
                        prihlasenie.Navigate(typeof(Aplikacia), null, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, prihlasenie);
                    }
                    break;
            }
        }
    }
}