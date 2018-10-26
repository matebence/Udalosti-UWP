using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.UI;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Udalosti.Udalosti.SpracovanieDat
{
    class SpravcaDat
    {
        public void zvolenaUdalost(Frame frame, ItemClickEventArgs e)
        {
            Debug.WriteLine("Metoda zvolenaUdalost bola vykonana");

            var zvolenaUdalost = (Udalost)e.ClickedItem;
            frame.Navigate(typeof(Podrobnosti), zvolenaUdalost);
        }

        public async Task nacitajZoznamAsync(UdalostiUdaje udalostiUdaje, ObservableCollection<Udalost> udalosti, Dictionary<string, string> pouzivatelskeUdaje, Dictionary<string, string> miestoPrihlasenia, ProgressRing nacitavanieUdalosti, bool pozicia)
        {
            Debug.WriteLine("Metoda nacitajZoznam bola vykonana");

            nacitavanieUdalosti.IsActive = true;
            nacitavanieUdalosti.Visibility = Visibility.Visible;

            if (udalosti.Count == 0)
            {
                if (pozicia)
                {
                    await udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], miestoPrihlasenia["okres"], miestoPrihlasenia["pozicia"], pouzivatelskeUdaje["token"]);
                }
                else
                {
                    await udalostiUdaje.zoznamUdalostiAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], pouzivatelskeUdaje["token"]);
                }
            }
        }

        public async Task nacitaveniaUdalostiAsync(UdalostiUdaje udalostiUdaje, List<Udalost> udaje, ObservableCollection<Udalost> udalosti, Image chybaUdalosti, ListView zoznamUdalosti)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalostiAsync bola vykonana");

            zoznamUdalosti.Visibility = Visibility.Visible;
            chybaUdalosti.Visibility = Visibility.Collapsed;

            await ziskajUdalostiAsync(udalostiUdaje, udaje, zoznamUdalosti, udalosti);
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