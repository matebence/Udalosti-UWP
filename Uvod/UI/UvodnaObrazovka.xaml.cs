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
using System;
using Udalosti.Nastroje;
using Udalosti.Dialog;

namespace Udalosti.Uvod.UI
{
    public sealed partial class UvodnaObrazovka : Page, KommunikaciaOdpoved
    {
        private AutentifkaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        public UvodnaObrazovka()
        {
            Debug.WriteLine("Metoda UvodnaObrazovka bola vykonana");

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

                Dictionary<string, double> poloha = await Lokalizator.zistiPolohuAsync();
                Dictionary<string, string> pouzivatelskeUdaje = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
                if (poloha == null)
                {
                    try
                    {
                        await this.autentifkaciaUdaje.miestoPrihlasenia(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["heslo"], false);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("CHYBA: " + ex.Message);
                        await DialogOznameni.kommunikaciaAsync("Chyba", "Server je momentalne nedostupný!", "Zatvoriť", false, null);
                    }
                }
                else
                {
                    try
                    {
                        await this.autentifkaciaUdaje.miestoPrihlasenia(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["heslo"], poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], false, false);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("CHYBA: " + ex.Message);
                        await DialogOznameni.kommunikaciaAsync("Chyba", "Server je momentalne nedostupný!", "Zatvoriť", false, null);
                    }
                }
            }
            else
            {
                this.Frame.Navigate(typeof(Prihlasenie), "chyba", new DrillInNavigationTransitionInfo());
            }
        }

        public void odpovedServer(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServer - UvodnaObrazovka bola vykonana");

            nacitavanie.IsActive = false;
            nacitavanie.Visibility = Visibility.Collapsed;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(udaje["email"], udaje["heslo"], udaje["token"]);
                        uvodnaObrazovka.Navigate(typeof(Aplikacia), new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        this.Frame.Navigate(typeof(Prihlasenie), "neUspesnePrihlasenie", new DrillInNavigationTransitionInfo());
                    }
                    break;
            }
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServeraAsync - UvodnaObrazovka bola vykonana");

            throw new NotImplementedException();
        }
    }
}