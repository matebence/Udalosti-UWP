using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.Zoznam;
using Udalosti.Uvod.Data;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class ZoznamUdalosti : Page, KommunikaciaData, KommunikaciaOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private AutentifkaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private Dictionary<string, string> pouzivatelskeUdaje;
        private Dictionary<string, string> miestoPrihlasenia;

        private ObservableCollection<Udalost> udalost;
        private ObservableCollection<Udalost> udalostPodlaPozicie;

        public ZoznamUdalosti()
        {
            this.InitializeComponent();
            this.init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda init - ZoznamUdalosti bola vykonana");

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.autentifkaciaUdaje = new AutentifkaciaUdaje(this);

            this.pouzivatelskeUdaje = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miestoPrihlasenia = this.udalostiUdaje.miestoPrihlasenia();

            this.udalost = new ObservableCollection<Udalost>();
            this.udalostPodlaPozicie = new ObservableCollection<Udalost>();
        }

        private async void odhlasitSa(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda odhlasitSa bola vykonana");

            await this.udalostiUdaje.odhlasenieAsync(this.pouzivatelskeUdaje["email"]);
        }

        private void aktualnyPivot(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Metoda aktualnyPivot bola vykonana");

            switch (moznostiObsahu.SelectedIndex)
            {
                case 0:
                    miesto.Text = this.miestoPrihlasenia["stat"];
                    break;
                case 1:
                    if (this.miestoPrihlasenia["pozicia"] == null)
                    {
                        miesto.Text = "Pozícia neurčená";
                    }
                    else
                    {
                        miesto.Text = "Okolie " + this.miestoPrihlasenia["pozicia"];
                    }
                    break;
            }
        }

        private async void aktualizujUdalosti(DependencyObject sender, object args)
        {
            Debug.WriteLine("Metoda aktualizujUdalosti bola vykonana");

            this.udalost.Clear();

            nacitavanieUdalosti.IsActive = true;
            nacitavanieUdalosti.Visibility = Visibility.Visible;

            await this.udalostiUdaje.zoznamUdalostiAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.pouzivatelskeUdaje["token"]);
        }

        private async void aktualizujUdalostiPodlaPozicie(DependencyObject sender, object args)
        {
            Debug.WriteLine("Metoda aktualizujUdalostiPodlaPozicie bola vykonana");

            this.udalostPodlaPozicie.Clear();

            nacitavaniePodlaPozicie.IsActive = true;
            nacitavaniePodlaPozicie.Visibility = Visibility.Visible;

            if (this.miestoPrihlasenia["pozicia"] == null)
            {
                Dictionary<string, double> poloha = await zistiPolohuAsync();
                await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(this.pouzivatelskeUdaje["email"], this.pouzivatelskeUdaje["heslo"], poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], true);
            }
            else
            {
                await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.miestoPrihlasenia["okres"], this.miestoPrihlasenia["pozicia"], this.pouzivatelskeUdaje["token"]);
            }
        }

        private void zvolenaUdalost(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine("Metoda zvolenaUdalost bola vykonana");

            var zvolenaUdalost = (Udalost)e.ClickedItem;
            this.Frame.Navigate(typeof(Podrobnosti), zvolenaUdalost);
        }

        private void zvolenaUdalostPodlaPozicie(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine("Metoda zvolenaUdalostPodlaPozicie bola vykonana");

            var zvolenaUdalost = (Udalost)e.ClickedItem;
            this.Frame.Navigate(typeof(Podrobnosti), zvolenaUdalost);
        }

        private async Task nacitaveniaUdalostiPodlaPozicieAsync(List<Udalost> udaje)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalostiPodlaPozicieAsync bola vykonana");

            zoznamUdalostiPodlaPozicie.Visibility = Visibility.Visible;
            ziadneUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;

            await ziskajUdalostiAsync(udaje, zoznamUdalostiPodlaPozicie, this.udalostPodlaPozicie);
        }

        private async void nacitajZoznamPodlaPozicii(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda nacitajZoznamPodlaPozicii bola vykonana");

            nacitavaniePodlaPozicie.IsActive = true;
            nacitavaniePodlaPozicie.Visibility = Visibility.Visible;

            if (this.udalostPodlaPozicie.Count == 0)
            {
                await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.miestoPrihlasenia["okres"], this.miestoPrihlasenia["pozicia"], this.pouzivatelskeUdaje["token"]);
            }
        }

        private async Task nacitaveniaUdalostiAsync(List<Udalost> udaje)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalostiAsync bola vykonana");

            zoznamUdalosti.Visibility = Visibility.Visible;
            ziadneUdalosti.Visibility = Visibility.Collapsed;

            await ziskajUdalostiAsync(udaje, zoznamUdalosti, this.udalost);
        }

        private async void nacitajZoznam(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda nacitajZoznam bola vykonana");

            nacitavanieUdalosti.IsActive = true;
            nacitavanieUdalosti.Visibility = Visibility.Visible;

            if (this.udalost.Count == 0)
            {
                await this.udalostiUdaje.zoznamUdalostiAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.pouzivatelskeUdaje["token"]);
            }
        }

        public async Task dataZoServeraAsync(string odpoved, string od, List<Udalost> udaje)
        {
            Debug.WriteLine("Metoda dataZoServeraAsync - ZoznamUdalosti bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            await this.nacitaveniaUdalostiAsync(udaje);
                        }
                        else
                        {
                            zoznamUdalosti.Visibility = Visibility.Collapsed;
                            ziadneUdalosti.Visibility = Visibility.Visible;
                        }
                    }
                    break;
                case Nastavenia.UDALOSTI_PODLA_POZICIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            await this.nacitaveniaUdalostiPodlaPozicieAsync(udaje);
                        }
                        else
                        {
                            zoznamUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;
                            ziadneUdalostiPodlaPozicie.Visibility = Visibility.Visible;
                        }
                    }
                    break;
            }
            nacitavaniePodlaPozicie.IsActive = false;
            nacitavaniePodlaPozicie.Visibility = Visibility.Collapsed;

            nacitavanieUdalosti.IsActive = false;
            nacitavanieUdalosti.Visibility = Visibility.Collapsed;
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda odpovedServera - ZoznamUdalosti bola vykonana");

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.udalostiUdaje.automatickePrihlasenieVypnute(this.pouzivatelskeUdaje["email"]);
                        udalosti.Navigate(typeof(Prihlasenie), null , new DrillInNavigationTransitionInfo());
                    }
                    break;
                case Nastavenia.UDALOSTI_AKTUALIZUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.miestoPrihlasenia.Clear();
                        this.miestoPrihlasenia = this.udalostiUdaje.miestoPrihlasenia();
                        await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.miestoPrihlasenia["okres"], this.miestoPrihlasenia["pozicia"], this.pouzivatelskeUdaje["token"]);
                    }
                    else
                    {

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

        private async Task ziskajUdalostiAsync(List<Udalost> udaje, ListView zoznam, ObservableCollection<Udalost> obsah)
        {
            Debug.WriteLine("Metoda ziskajUdalostiAsync bola vykonana");

            foreach (Udalost __o in udaje)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await obrazokJeDostupnnyAsync(obrazok)))
                {
                    __o.obrazok = "../../Assets/Images/udalosti_chyba_obrazka.jpg";
                }
                else
                {
                    __o.obrazok = App.udalostiAdresa + __o.obrazok;
                }

                __o.mesiac = dlzkaSlova(__o.mesiac, 4);
                __o.mesto = __o.mesto + ", ";
                
                obsah.Add(__o);
            }
            zoznam.ItemsSource = obsah;
        }

        private string dlzkaSlova(string slovo, int dlzka)
        {
            Debug.WriteLine("Metoda dlzkaSlova bola vykonana");

            if (slovo.Length > dlzka)
            {
                return slovo.Substring(0, 3) + ".";
            }
            else
            {
                return slovo;
            }
        }

        public async Task<bool> obrazokJeDostupnnyAsync(string adresa)
        {
            Debug.WriteLine("Metoda obrazokJeDostupnnyAsync bola vykonana");

            WebRequest request = WebRequest.Create(App.udalostiAdresa + adresa);
            WebResponse odpoved;
            try
            {
                odpoved = await request.GetResponseAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}