using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Udalosti.Udalosti.SpracovanieDat
{
    class SpravcaDat
    {
        public async Task nacitajZoznamAsync(UdalostiUdaje udalostiUdaje, ObservableCollection<Udalost> obsah, Dictionary<string, string> pouzivatelskeUdaje, Dictionary<string, string> miestoPrihlasenia, ProgressRing nacitavanie, string karta)
        {
            Debug.WriteLine("Metoda nacitajZoznam bola vykonana");

            nacitavanie.IsActive = true;
            nacitavanie.Visibility = Visibility.Visible;

            if (obsah.Count == 0)
            {
                if (karta.Equals("Udalosti"))
                {
                    await udalostiUdaje.zoznamUdalostiAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], pouzivatelskeUdaje["token"]);
                }
                else if (karta.Equals("UdalostiPodlaPozicie"))
                {
                    await udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], miestoPrihlasenia["okres"], miestoPrihlasenia["pozicia"], pouzivatelskeUdaje["token"]);
                }
                else if (karta.Equals("Zaujmy"))
                {
                    await udalostiUdaje.zoznamZaujmovAsync(pouzivatelskeUdaje["email"], pouzivatelskeUdaje["token"]);
                }
            }
        }

        public async Task nacitaveniaUdalostiAsync(UdalostiUdaje udalostiUdaje, List<Udalost> udaje, ObservableCollection<Udalost> udalosti, Image obrazok, ListView zoznam)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalostiAsync bola vykonana");

            zoznam.Visibility = Visibility.Visible;
            obrazok.Visibility = Visibility.Collapsed;

            await ziskajUdalostiAsync(udalostiUdaje, udaje, zoznam, udalosti);
        }

        private async Task ziskajUdalostiAsync(UdalostiUdaje udalostiUdaje, List<Udalost> udaje, ListView zoznam, ObservableCollection<Udalost> obsah)
        {
            Debug.WriteLine("Metoda ziskajUdalostiAsync bola vykonana");

            foreach (Udalost __o in udaje)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await udalostiUdaje.obrazokJeDostupnnyAsync(obrazok, false)))
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
    }
}