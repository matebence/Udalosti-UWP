using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.SpracovanieDat;
using Udalosti.Udalosti.Zoznam;
using Udalosti.Uvod.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class Aplikacia : Page, KommunikaciaData, KommunikaciaOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private AutentifkaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private SpravcaDat spravcaDat;

        private Dictionary<string, string> pouzivatelskeUdaje;
        private Dictionary<string, string> miestoPrihlasenia;

        private ObservableCollection<Udalost> udalost;
        private ObservableCollection<Udalost> udalostPodlaPozicie;

        public Aplikacia()
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

            this.spravcaDat = new SpravcaDat();

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
                    titul.Text = this.miestoPrihlasenia["stat"];
                    break;
                case 1:
                    if (this.miestoPrihlasenia["pozicia"] == null)
                    {
                        titul.Text = "Pozícia neurčená";
                    }
                    else
                    {
                        titul.Text = "Okolie " + this.miestoPrihlasenia["pozicia"];
                    }
                    break;
                case 2:
                    titul.Text = "Zoznam záujmov";
                    break;
            }
        }

        private void zvolenaUdalost(object sender, ItemClickEventArgs e)
        {
            spravcaDat.zvolenaUdalost(this.Frame, e);
        }

        private void zvolenaUdalostPodlaPozicie(object sender, ItemClickEventArgs e)
        {
            spravcaDat.zvolenaUdalost(this.Frame, e);
        }

        private async void nacitajZoznamPodlaPozicii(object sender, RoutedEventArgs e)
        {
            await spravcaDat.nacitajZoznamAsync(udalostiUdaje, udalost, pouzivatelskeUdaje, miestoPrihlasenia, nacitavaniePodlaPozicie, true);
        }

        private async void nacitajZoznam(object sender, RoutedEventArgs e)
        {
            await spravcaDat.nacitajZoznamAsync(udalostiUdaje, udalost, pouzivatelskeUdaje, miestoPrihlasenia, nacitavanieUdalosti, false);
        }

        private async void aktualizujUdalosti(DependencyObject sender, object args)
        {
            Debug.WriteLine("Metoda aktualizujUdalosti bola vykonana");

            this.udalost.Clear();

            chybaUdalosti.Visibility = Visibility.Collapsed;
            nacitavanieUdalosti.IsActive = true;
            nacitavanieUdalosti.Visibility = Visibility.Visible;

            await this.udalostiUdaje.zoznamUdalostiAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.pouzivatelskeUdaje["token"]);
        }

        private async void aktualizujUdalostiPodlaPozicie(DependencyObject sender, object args)
        {
            Debug.WriteLine("Metoda aktualizujUdalostiPodlaPozicie bola vykonana");

            this.udalostPodlaPozicie.Clear();

            chybaUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;
            nacitavaniePodlaPozicie.IsActive = true;
            nacitavaniePodlaPozicie.Visibility = Visibility.Visible;

            if (this.miestoPrihlasenia["pozicia"] == null)
            {
                Dictionary<string, double> poloha = await Lokalizator.zistiPolohuAsync();
                if (poloha == null)
                {
                    await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.miestoPrihlasenia["okres"], this.miestoPrihlasenia["pozicia"], this.pouzivatelskeUdaje["token"]);
                }
                else
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(this.pouzivatelskeUdaje["email"], this.pouzivatelskeUdaje["heslo"], poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], true);
                }
            }
            else
            {
                await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.miestoPrihlasenia["okres"], this.miestoPrihlasenia["pozicia"], this.pouzivatelskeUdaje["token"]);
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
                        chybaUdalosti.Visibility = Visibility.Collapsed;

                        if (udaje != null)
                        {
                            await spravcaDat.nacitaveniaUdalostiAsync(udalostiUdaje, udaje, udalost, chybaUdalosti, zoznamUdalosti);
                        }
                        else
                        {
                            chybaUdalosti.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_udalosti.png"));

                            zoznamUdalosti.Visibility = Visibility.Collapsed;
                            chybaUdalosti.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        chybaUdalosti.Visibility = Visibility.Visible;
                        chybaUdalosti.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_spojenie_zlyhalo.png"));
                    }
                    break;
                case Nastavenia.UDALOSTI_PODLA_POZICIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        chybaUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;

                        if (udaje != null)
                        {
                            await spravcaDat.nacitaveniaUdalostiAsync(udalostiUdaje, udaje, udalostPodlaPozicie, chybaUdalostiPodlaPozicie, zoznamUdalostiPodlaPozicie);
                        }
                        else
                        {
                            chybaUdalostiPodlaPozicie.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_udalosti.png"));

                            zoznamUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;
                            chybaUdalostiPodlaPozicie.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        chybaUdalostiPodlaPozicie.Visibility = Visibility.Visible;
                        chybaUdalostiPodlaPozicie.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_spojenie_zlyhalo.png"));
                    }
                    break;
            }
            nacitavaniePodlaPozicie.IsActive = false;
            nacitavaniePodlaPozicie.Visibility = Visibility.Collapsed;

            nacitavanieUdalosti.IsActive = false;
            nacitavanieUdalosti.Visibility = Visibility.Collapsed;
        }

        public async Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
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
                    break;
            }
        }
    }
}