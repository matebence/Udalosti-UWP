using System.Collections.ObjectModel;
using System.Net;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Udalosti.Udalosti.Data;
using Udalosti.Udaje.Siet;
using System.Collections.Generic;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Uvod.Data;
using System.Diagnostics;
using Windows.UI.Xaml;
using System.Text;
using Windows.UI.Xaml.Media.Animation;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class ZoznamUdalosti : Page, KommunikaciaData, KommunikaciaOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private Dictionary<string, string> pouzivatelskeUdaje;
        private Dictionary<string, string> miestoPrihlasenia;

        private ObservableCollection<Udalost> udalost;
        private ObservableCollection<Udalost> udalostPodlaPozicie;

        public ZoznamUdalosti()
        {
            this.InitializeComponent();

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            this.pouzivatelskeUdaje = uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miestoPrihlasenia = udalostiUdaje.miestoPrihlasenia();

            this.udalost = new ObservableCollection<Udalost>();
            this.udalostPodlaPozicie = new ObservableCollection<Udalost>();
        }

        public async Task dataZoServeraAsync(string odpoved, string od, List<Udalost> udaje)
        {
            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            nacitavanieUdalosti.IsActive = false;
                            nacitavanieUdalosti.Visibility = Visibility.Collapsed;

                            zoznamUdalosti.Visibility = Visibility.Visible;
                            ziadneUdalosti.Visibility = Visibility.Collapsed;

                            await ziskajUdalostiAsync(udaje, zoznamUdalosti, udalost);
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
                            nacitavaniePodlaPozicie.IsActive = false;
                            nacitavaniePodlaPozicie.Visibility = Visibility.Collapsed;

                            zoznamUdalostiPodlaPozicie.Visibility = Visibility.Visible;
                            ziadneUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;

                            await ziskajUdalostiAsync(udaje, zoznamUdalostiPodlaPozicie, udalostPodlaPozicie);
                        }
                        else
                        {
                            zoznamUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;
                            ziadneUdalostiPodlaPozicie.Visibility = Visibility.Visible;
                        }
                    }
                    break;
            }
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        udalostiUdaje.automatickePrihlasenieVypnute(pouzivatelskeUdaje["email"]);
                        udalosti.Navigate(typeof(Prihlasenie), null , new DrillInNavigationTransitionInfo());
                    }
                    break;
            }
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
                obsah.Add(__o);
            }
            zoznam.ItemsSource = obsah;
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

        private void aktualnyPivot(object sender, SelectionChangedEventArgs e)
        {
            switch (moznostiObsahu.SelectedIndex)
            {
                case 0:
                    miesto.Text = miestoPrihlasenia["stat"];
                    break;
                case 1:
                    miesto.Text = miestoPrihlasenia["mesto"];
                    break;
            }
        }

        private async void odhlasitSa(object sender, RoutedEventArgs e)
        {
            await udalostiUdaje.odhlasenieAsync(pouzivatelskeUdaje["email"]);
        }

        private async void nacitajZoznamPodlaPozicii(object sender, RoutedEventArgs e)
        {
            nacitavaniePodlaPozicie.IsActive = true;
            nacitavaniePodlaPozicie.Visibility = Visibility.Visible;

            await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], miestoPrihlasenia["okres"], miestoPrihlasenia["mesto"], pouzivatelskeUdaje["token"]);
        }

        private async void nacitajZoznam(object sender, RoutedEventArgs e)
        {
            nacitavanieUdalosti.IsActive = true;
            nacitavanieUdalosti.Visibility = Visibility.Visible;

            await this.udalostiUdaje.zoznamUdalostiAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], pouzivatelskeUdaje["token"]);
        }
    }
}