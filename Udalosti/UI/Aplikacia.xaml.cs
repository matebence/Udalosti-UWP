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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Udalosti.Dialog;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class Aplikacia : Page, KommunikaciaData, KommunikaciaOdpoved, Aktualizator, DialogOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private AutentifkaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private SpravcaDat spravcaDat;

        private Dictionary<string, string> pouzivatelskeUdaje, miestoPrihlasenia;
        private ObservableCollection<Udalost> udalostiZoznam, udalostiPodlaPozicie, zaujmy;

        private int idUdalost;

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
            AktualizatorObsahu.zaujmy().nastav(this);

            this.pouzivatelskeUdaje = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miestoPrihlasenia = this.udalostiUdaje.miestoPrihlasenia();

            this.udalostiZoznam = new ObservableCollection<Udalost>();
            this.udalostiPodlaPozicie = new ObservableCollection<Udalost>();
            this.zaujmy = new ObservableCollection<Udalost>();
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
            Debug.WriteLine("Metoda zvolenaUdalost bola vykonana");

            var zvolenaUdalost = (Udalost)e.ClickedItem;
            this.Frame.Navigate(typeof(Podrobnosti), zvolenaUdalost);
        }

        private void spustiMoznostiZoznamu(object sender, RightTappedRoutedEventArgs e)
        {
            Debug.WriteLine("Metoda spustiMoznostiZoznamu bola vykonana");

            ListView zoznam = (ListView)sender;
            moznostiZoznamu.ShowAt(zoznam, e.GetPosition(zoznam));
            var prvok = ((FrameworkElement)e.OriginalSource).DataContext as Udalost;
            this.idUdalost = prvok.idUdalost;
        }

        private void odstranitZaujem(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda odstranitZaujem bola vykonana");

            if (this.idUdalost != -1)
            { 
                foreach (var udalost in this.zaujmy)
                {
                    if (udalost.idUdalost == this.idUdalost)
                    {
                        DialogPotvrdeni.potvrd(
                            "Odstránenie záujmov", "Naozaj chcete odstránit záujem " + udalost.nazov + "?", 
                            "Áno odstrániť", "Nie",
                            this, udalost);
                    }
                }
            }
            this.idUdalost = -1;
        }

        private async void nacitajUdalosti(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda nacitajUdalosti bola vykonana");

            await spravcaDat.nacitajZoznamAsync(this.udalostiUdaje, this.udalostiZoznam, this.pouzivatelskeUdaje, this.miestoPrihlasenia, nacitavaniePodlaPozicie, "Udalosti");
        }

        private async void nacitajUdalostiPodlaPozicie(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda nacitajUdalostiPodlaPozicie bola vykonana");

            await spravcaDat.nacitajZoznamAsync(this.udalostiUdaje, this.udalostiPodlaPozicie, pouzivatelskeUdaje, miestoPrihlasenia, nacitavanieUdalosti, "UdalostiPodlaPozicie");
        }

        private async void nacitajZaujmy(object sender, RoutedEventArgs e)
        {
            await spravcaDat.nacitajZoznamAsync(this.udalostiUdaje, this.zaujmy, this.pouzivatelskeUdaje, this.miestoPrihlasenia, nacitavanieZaujmov, "Zaujmy");
        }

        private async void aktualizujUdalosti(DependencyObject sender, object args)
        {
            Debug.WriteLine("Metoda aktualizujUdalosti bola vykonana");

            this.udalostiZoznam.Clear();

            chybaUdalosti.Visibility = Visibility.Collapsed;
            nacitavanieUdalosti.IsActive = true;
            nacitavanieUdalosti.Visibility = Visibility.Visible;

            await this.udalostiUdaje.zoznamUdalostiAsync(this.pouzivatelskeUdaje["email"], this.miestoPrihlasenia["stat"], this.pouzivatelskeUdaje["token"]);
        }

        private async void aktualizujUdalostiPodlaPozicie(DependencyObject sender, object args)
        {
            Debug.WriteLine("Metoda aktualizujUdalostiPodlaPozicie bola vykonana");

            this.udalostiPodlaPozicie.Clear();

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
                            await spravcaDat.nacitaveniaUdalostiAsync(this.udalostiUdaje, udaje, this.udalostiZoznam, chybaUdalosti, zoznamUdalosti);
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
                    nacitavanieUdalosti.IsActive = false;
                    nacitavanieUdalosti.Visibility = Visibility.Collapsed;

                    break;
                case Nastavenia.UDALOSTI_PODLA_POZICIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        chybaUdalostiPodlaPozicie.Visibility = Visibility.Collapsed;

                        if (udaje != null)
                        {
                            await spravcaDat.nacitaveniaUdalostiAsync(this.udalostiUdaje, udaje, this.udalostiPodlaPozicie, chybaUdalostiPodlaPozicie, zoznamUdalostiPodlaPozicie);
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
                    nacitavaniePodlaPozicie.IsActive = false;
                    nacitavaniePodlaPozicie.Visibility = Visibility.Collapsed;

                    break;
                case Nastavenia.ZAUJEM_ZOZNAM:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        chybaZaujmov.Visibility = Visibility.Collapsed;

                        if (udaje != null)
                        {
                            await spravcaDat.nacitaveniaUdalostiAsync(this.udalostiUdaje, udaje, this.zaujmy, chybaZaujmov, zoznamZaujmov);
                        }
                        else
                        {
                            chybaZaujmov.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_zaujmy.png"));

                            zoznamZaujmov.Visibility = Visibility.Collapsed;
                            chybaZaujmov.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        chybaZaujmov.Visibility = Visibility.Visible;
                        chybaZaujmov.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_spojenie_zlyhalo.png"));
                    }
                    nacitavanieZaujmov.IsActive = false;
                    nacitavanieZaujmov.Visibility = Visibility.Collapsed;

                    break;
                    }
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
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, udalosti);
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
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, udalosti);
                    }
                    break;

                case Nastavenia.ZAUJEM_ODSTRANENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje["uspech"] != null)
                        {
                            if (zaujmy.Count == 1)
                            {
                                chybaZaujmov.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_zaujmy.png"));

                                zoznamZaujmov.Visibility = Visibility.Collapsed;
                                chybaZaujmov.Visibility = Visibility.Visible;
                            }
                        }
                        else if (udaje["chyba"] != null)
                        {
                            await DialogOznameni.kommunikaciaAsync("Chyba", udaje["chyba"], "Zatvoriť", false, udalosti);
                        }
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, udalosti);
                    }
                    break;
            }
        }

        public async void aktualizujObsahZaujmov()
        {
            Debug.WriteLine("Metoda aktualizujObsahZaujmov bola vykonana");

            zaujmy.Clear();
            await spravcaDat.nacitajZoznamAsync(this.udalostiUdaje, this.zaujmy, this.pouzivatelskeUdaje, this.miestoPrihlasenia, nacitavanieZaujmov, "Zaujmy");
        }

        public async void tlacidloA(Udalost udalost)
        {
            Debug.WriteLine("Metoda DialogPotvrdeni - tlacidloA bola vykonana");

            await this.udalostiUdaje.odstranZaujemAsync(this.pouzivatelskeUdaje["email"], this.pouzivatelskeUdaje["token"], udalost.idUdalost);
            this.zaujmy.Remove(udalost);
        }

        public void tlacidloB(Udalost udalost)
        {
            Debug.WriteLine("Metoda DialogPotvrdeni - tlacidloB bola vykonana");

            Debug.WriteLine("Odstranenie zrusene");
        }
    }
}