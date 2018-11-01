using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Dialog;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.SpracovanieDat;
using Udalosti.Udalosti.Zoznam;
using Udalosti.Uvod.Data;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class Podrobnosti : Page, KommunikaciaData, KommunikaciaOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private Dictionary<string, string> pouzivatelskeUdaje;

        private int stavTlacidla;
        private int idUdalost;
        private int pocetZaujemcov;

        public Podrobnosti()
        {
            Debug.WriteLine("Metoda Podrobnosti bola vykonana");

            this.InitializeComponent();
            this.init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda init - Podrobnosti bola vykonana");

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            this.pouzivatelskeUdaje = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();

            var uwpSpat = SystemNavigationManager.GetForCurrentView();
            uwpSpat.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("Metoda OnNavigatedTo - Podrobnosti bola vykonana");

            base.OnNavigatedTo(e);

            var aktualnaUdalost = (Udalost)e.Parameter;

            await spracujZvolenuUdalost(aktualnaUdalost);

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

        private async void zaujem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda zaujem bola vykonana");

            if (this.stavTlacidla == 1)
            {
                this.stavTlacidla = 0;
                await this.udalostiUdaje.odstranZaujem(this.pouzivatelskeUdaje["email"], this.pouzivatelskeUdaje["token"], idUdalost);

            }
            else
            {
                this.stavTlacidla = 1;
                await this.udalostiUdaje.zaujem(this.pouzivatelskeUdaje["email"], this.pouzivatelskeUdaje["token"], idUdalost);
            }
        }

        public async Task dataZoServera(string odpoved, string od, List<Udalost> udaje)
        {
            Debug.WriteLine("Metoda dataZoServera - Podrobnosti bola vykonana");

            switch (od)
            {
                case Nastavenia.ZAUJEM_POTVRD:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            if (udaje.Count == 1)
                            {

                                Udalost udalost = (Udalost)udaje[0];
                                nacitajUdalosti(udalost, false, App.udalostiAdresa);
                            }
                        }
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, null);
                    }
                    break;
            }
        }

        public async Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServeraAsync - Podrobnosti bola vykonana");

            switch (od)
            {
                case Nastavenia.ZAUJEM_ODSTRANENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje["uspech"] != null)
                        {
                            this.pocetZaujemcov--;
                            pocetZaujemcovZvolenejUdalosti.Text = pocetZaujemcov.ToString();
                            AktualizatorObsahu.zaujmy().hodnota();

                            tlacidloZvolenejUdalosti.Content = "Mám záujem";
                            tlacidloZvolenejUdalosti.Background = new SolidColorBrush(Color.FromArgb(255, 0, 91, 166));
                        }
                        else
                        {
                            await DialogOznameni.kommunikaciaAsync("Chyba", udaje["chyba"], "Zatvoriť", false, null);
                        }
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, null);
                    }
                    break;

                case Nastavenia.ZAUJEM:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje["uspech"] != null)
                        {
                            this.pocetZaujemcov++;
                            pocetZaujemcovZvolenejUdalosti.Text = pocetZaujemcov.ToString();
                            AktualizatorObsahu.zaujmy().hodnota();

                            tlacidloZvolenejUdalosti.Content = "Odstrániť zo záujmov";
                            tlacidloZvolenejUdalosti.Background = new SolidColorBrush(Color.FromArgb(255, 171, 39, 39));
                        }
                        else
                        {
                            await DialogOznameni.kommunikaciaAsync("Chyba", udaje["chyba"], "Zatvoriť", false, null);
                        }
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, null);
                    }
                    break;
            }
        }

        public void odpovedServer(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServer bola vykonana");

            throw new NotImplementedException();
        }

        private async Task spracujZvolenuUdalost(Udalost udalost)
        {
            Debug.WriteLine("Metoda spracujZvolenuUdalost bola vykonana");

            if (udalost != null)
            {
                this.stavTlacidla = udalost.zaujem;
                this.idUdalost = udalost.idUdalost;
                this.pocetZaujemcov = udalost.zaujemcovia;

                nacitavanie.Visibility = Visibility.Visible;
                obsah.Visibility = Visibility.Collapsed;
                tlacidloZvolenejUdalosti.Visibility = Visibility.Collapsed;

                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    await this.udalostiUdaje.potvrdZaujem(this.pouzivatelskeUdaje["email"], this.pouzivatelskeUdaje["token"], udalost.idUdalost);
                }
                else
                {
                    nacitajUdalosti(udalost, true, "");
                }
            }
        }

        private async void nacitajUdalosti(Udalost udalost, bool server, String host)
        {
            Debug.WriteLine("Metoda nacitajUdalosti bola vykonana");

            if (!(await this.udalostiUdaje.obrazokJeDostupnny(udalost.obrazok, server)))
            {
                obrazokZvolenejUdalosti.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_chyba_obrazka.jpg"));
            }
            else
            {
                obrazokZvolenejUdalosti.Source = new BitmapImage(new Uri(host+udalost.obrazok));
            }

            this.stavTlacidla = udalost.zaujem;

            denZvolenejUdalosti.Text = udalost.den;
            mesiacZvolenejUdalosti.Text = udalost.mesiac.Substring(0, 3) + ".";
            nazovZvolenejUdalosti.Text = udalost.nazov;
            pocetZaujemcovZvolenejUdalosti.Text = udalost.zaujemcovia.ToString();
            vstupenkaZvolenejUdalosti.Text = udalost.vstupenka.ToString() + "€";
            casZvolenejUdalosti.Text = udalost.cas;

            if (server)
            {
                miestoZvolenejUdalosti.Text = udalost.mesto + udalost.ulica;
            }
            else
            {
                miestoZvolenejUdalosti.Text = udalost.mesto + ", " + udalost.ulica;
            }

            nastavTlacdloPodrobnosti(this.stavTlacidla);
        }

        private void nastavTlacdloPodrobnosti(int stavTlacidla)
        {
            Debug.WriteLine("Metoda nastavTlacdloPodrobnosti bola vykonana");

            if (stavTlacidla == 1)
            {
                tlacidloZvolenejUdalosti.Content = "Odstrániť zo záujmov";
                tlacidloZvolenejUdalosti.Background = new SolidColorBrush(Color.FromArgb(255, 171, 39, 39));
            }
            else
            {
                tlacidloZvolenejUdalosti.Content = "Mám záujem";
                tlacidloZvolenejUdalosti.Background = new SolidColorBrush(Color.FromArgb(255, 0, 91, 166));
            }

            tlacidloZvolenejUdalosti.Visibility = Visibility.Visible;
            obsah.Visibility = Visibility.Visible;
            nacitavanie.Visibility = Visibility.Collapsed;
        }
    }
}