using System.Collections.ObjectModel;
using System.Net;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Udalosti.Udalosti.Data;
using Udalosti.Udaje.Siet;
using System.Collections;
using System.Collections.Generic;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Uvod.Data;
using System.Diagnostics;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class ZoznamUdalosti : Page, KommunikaciaData, KommunikaciaOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private Dictionary<string, string> pouzivatelskeUdaje;
        private Dictionary<string, string> miestoPrihlasenia;

        public ZoznamUdalosti()
        {
            this.InitializeComponent();

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
    
            this.pouzivatelskeUdaje = uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miestoPrihlasenia = udalostiUdaje.miestoPrihlasenia();
        }

        public async Task dataZoServeraAsync(string odpoved, string od, ArrayList udaje)
        {
            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            await ziskajUdalostiAsync(udaje);
                        }
                        else
                        {
                        }
                    }
                    break;
            }
        }

        public Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            throw new System.NotImplementedException();
        }

        private async Task ziskajUdalostiAsync(ArrayList udaje)
        {
            Debug.WriteLine("Metoda ziskajUdalostiAsync bola vykonana");

            ObservableCollection<Udalost> udalosti = new ObservableCollection<Udalost>();

            foreach (Udalost __o in udaje)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await obrazokJeDostupnnyAsync(obrazok))) {
                    __o.obrazok = "../../Assets/Images/udalosti_chyba_obrazka.jpg";
                }
                udalosti.Add(__o);
            }
            zoznamUdalosti.ItemsSource = udalosti;
        }

        private async void nacitajZoznam(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda nacitajZoznam bola vykonana");

            await this.udalostiUdaje.zoznamUdalostiAsync(pouzivatelskeUdaje["email"], miestoPrihlasenia["stat"], pouzivatelskeUdaje["token"]);
        }

        public async Task<bool> obrazokJeDostupnnyAsync(string adresa)
        {
            Debug.WriteLine("Metoda obrazokJeDostupnnyAsync bola vykonana");

            WebRequest request = WebRequest.Create(adresa);
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