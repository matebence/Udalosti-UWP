using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Udalosti.Udalosti.SpracovanieDat
{
    class SpravcaDat
    {
        private static ObservableCollection<Udalost> udalosti, udalostiPodlaPozicie, zaujmy = null;

        public async Task nacitajZoznam(UdalostiUdaje udalostiUdaje, Dictionary<string, string> pouzivatelskeUdaje, Dictionary<string, string> miestoPrihlasenia, ProgressRing nacitavanie, ListView zoznam, Image chyba, string karta)
        {
        Debug.WriteLine("Metoda nacitajZoznam bola vykonana");

        nacitavanie.IsActive = true;
        nacitavanie.Visibility = Visibility.Visible;

            if (karta.Equals("Udalosti"))
            {
                if (SpravcaDat.getUdalosti() == null)
                {
                    SpravcaDat.setUdalosti(false);
                    await udalostiUdaje.zoznamUdalosti(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], pouzivatelskeUdaje["token"]);
                }
                else
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        if (SpravcaDat.getUdalosti().Count < 1)
                        {
                            nacitavanie.Visibility = Visibility.Collapsed;
                            chyba.Visibility = Visibility.Visible;
                            chyba.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_udalosti.png"));
                        }
                        else
                        {
                            zoznam.ItemsSource = SpravcaDat.getUdalosti();

                            nacitavanie.Visibility = Visibility.Collapsed;
                            zoznam.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        nacitavanie.Visibility = Visibility.Collapsed;
                        chyba.Visibility = Visibility.Visible;
                        chyba.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_spojenie_zlyhalo.png"));
                    }
                    
                }
            }
            else if (karta.Equals("Lokalizator"))
            {
                if (SpravcaDat.getUdalostiPodlaPozicie() == null)
                {
                    SpravcaDat.setUdalostiPodlaPozicie(false);
                    await udalostiUdaje.zoznamUdalostiPodlaPozicie(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], miestoPrihlasenia["okres"], miestoPrihlasenia["pozicia"], pouzivatelskeUdaje["token"]);
                }
                else
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        if (SpravcaDat.getUdalostiPodlaPozicie().Count < 1)
                        {
                            nacitavanie.Visibility = Visibility.Collapsed;
                            chyba.Visibility = Visibility.Visible;

                            chyba.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_udalosti.png"));
                        }
                        else
                        {
                            zoznam.ItemsSource = SpravcaDat.getUdalostiPodlaPozicie();

                            nacitavanie.Visibility = Visibility.Collapsed;
                            zoznam.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        nacitavanie.Visibility = Visibility.Collapsed;
                        chyba.Visibility = Visibility.Visible;

                        chyba.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_spojenie_zlyhalo.png"));
                    }
                }
            }
            else if (karta.Equals("Zaujmy"))
            {
                if (SpravcaDat.getZaujmy() == null)
                {
                    SpravcaDat.setZaujmy(false);
                    await udalostiUdaje.zoznamZaujmov(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["token"]);
                }
                else
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        if (SpravcaDat.getZaujmy().Count < 1)
                        {
                            nacitavanie.Visibility = Visibility.Collapsed;
                            chyba.Visibility = Visibility.Visible;

                            chyba.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_ziadne_zaujmy.png"));
                        }
                        else
                        {
                            zoznam.ItemsSource = SpravcaDat.getZaujmy();

                            nacitavanie.Visibility = Visibility.Collapsed;
                            zoznam.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        nacitavanie.Visibility = Visibility.Collapsed;
                        chyba.Visibility = Visibility.Visible;

                        chyba.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/udalosti_spojenie_zlyhalo.png"));
                    }
                }
            }
        }

        public async Task nacitaveniaUdalosti(UdalostiUdaje udalostiUdaje, List<Udalost> udaje, ObservableCollection<Udalost> udalosti, Image obrazok, ListView zoznam)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalosti bola vykonana");

            zoznam.Visibility = Visibility.Visible;
            obrazok.Visibility = Visibility.Collapsed;

            await ziskajUdalosti(udalostiUdaje, udaje, zoznam, udalosti);
        }

        private async Task ziskajUdalosti(UdalostiUdaje udalostiUdaje, List<Udalost> udaje, ListView zoznam, ObservableCollection<Udalost> obsah)
        {
            Debug.WriteLine("Metoda ziskajUdalosti bola vykonana");

            foreach (Udalost __o in udaje)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await udalostiUdaje.obrazokJeDostupnny(obrazok, false)))
                {
                    __o.obrazok = "../../Assets/Images/udalosti_chyba_obrazka.jpg";
                }
                else
                {
                    __o.obrazok = App.udalostiAdresa + __o.obrazok;
                }

                __o.mesiac = dlzkaSlova(__o.mesiac, 4);
                __o.mesto = __o.mesto + ", ";
                __o.den = __o.den + ".";

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

        public static ObservableCollection<Udalost> getUdalosti()
        {
            Debug.WriteLine("Metoda getUdalosti bola vykonana");

            return SpravcaDat.udalosti;
        }

        public static void setUdalosti(bool reset)
        {
            Debug.WriteLine("Metoda setUdalosti bola vykonana");

            if (reset)
            {
                SpravcaDat.udalosti = null;
            }
            else
            {
                SpravcaDat.udalosti = new ObservableCollection<Udalost>();
            }
        }

        public static ObservableCollection<Udalost> getUdalostiPodlaPozicie()
        {
            Debug.WriteLine("Metoda getUdalostiPodlaPozicie bola vykonana");

            return SpravcaDat.udalostiPodlaPozicie;
        }

        public static void setUdalostiPodlaPozicie(bool reset)
        {
            Debug.WriteLine("Metoda setUdalostiPodlaPozicie bola vykonana");

            if (reset)
            {
                SpravcaDat.udalostiPodlaPozicie = null;
            }
            else
            {
                SpravcaDat.udalostiPodlaPozicie = new ObservableCollection<Udalost>();
            }
        }

        public static ObservableCollection<Udalost> getZaujmy()
        {
            Debug.WriteLine("Metoda getZaujmy bola vykonana");

            return SpravcaDat.zaujmy;
        }

        public static void setZaujmy(bool reset)
        {
            Debug.WriteLine("Metoda setZaujmy bola vykonana");

            if (reset)
            {
                SpravcaDat.zaujmy = null;
            }
            else
            {
                SpravcaDat.zaujmy = new ObservableCollection<Udalost>();
            }
        }
    }
}