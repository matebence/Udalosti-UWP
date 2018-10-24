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
using Windows.Devices.Geolocation;
using System;

namespace Udalosti.Uvod.UI
{
    public sealed partial class UvodnaObrazovka : Page, KommunikaciaOdpoved
    {
        private AutentifkaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        public UvodnaObrazovka()
        {
            this.InitializeComponent();
            this.init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda init - UvodnaObrazovka bola vykonana");

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.autentifkaciaUdaje = new AutentifkaciaUdaje(this);
        }

        private async void automatickePrihlasenie(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda automatickePrihlasenie bola vykonana");
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                Dictionary<string, double> poloha = await zistiPolohuAsync();
                Dictionary<string, string> pouzivatelskeUdaje = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
                if (poloha == null)
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["heslo"]);
                }
                else
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["heslo"], poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], false);
                }
            }
            else
            {
                this.Frame.Navigate(typeof(Prihlasenie), "chyba", new DrillInNavigationTransitionInfo());
            }
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServera - UvodnaObrazovka bola vykonana");

            nacitavanie.IsActive = false;
            nacitavanie.Visibility = Visibility.Collapsed;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(udaje["email"], udaje["heslo"], udaje["token"]);
                        uvodnaObrazovka.Navigate(typeof(ZoznamUdalosti), new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(Prihlasenie), "neUspesnePrihlasenie", new DrillInNavigationTransitionInfo());
                    }
                    break;
            }
        }

        private async Task<Dictionary<string, double>> zistiPolohuAsync()
        {
            Debug.WriteLine("Metoda zistiPolohu bola vykonana");

            var pozicia = new Geolocator();
            pozicia.DesiredAccuracy = PositionAccuracy.High;
            Geoposition geo = await pozicia.GetGeopositionAsync(maximumAge: TimeSpan.FromSeconds(Nastavenia.DLZKA_REQUESTU), timeout: TimeSpan.FromSeconds(1));

            Dictionary<string, double> poloha;
            if (geo != null)
            {
                poloha = new Dictionary<string, double>();
                poloha.Add("zemepisnaSirka", geo.Coordinate.Point.Position.Latitude);
                poloha.Add("zemepisnaDlzka", geo.Coordinate.Point.Position.Longitude);
            }
            else
            {
                poloha = null;
            }

            return poloha;
        }
    }
}