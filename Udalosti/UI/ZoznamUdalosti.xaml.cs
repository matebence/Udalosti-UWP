using System.Collections.ObjectModel;
using System.Net;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace Udalosti.Udalosti.UI
{
    public sealed partial class ZoznamUdalosti : Page
    {
        public ZoznamUdalosti()
        {
            this.InitializeComponent();
        }

        public async Task<bool> obrazokJeDostupnnyAsync(string adresa)
        {
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

        private async void nacitajZoznam(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string adresa = "https://bmate18.student.ki.fpv.ukf.sk/udalosti/uploads/g.jpg";
            if (await obrazokJeDostupnnyAsync(adresa))
            {
                adresa = "../../Assets/Images/udalosti_chyba_obrazka.jpg";
            }

            ObservableCollection<Udalost> udalosti = new ObservableCollection<Udalost>();
            Udalost udalost = new Udalost() { obrazok = adresa, den = " 25.", mesiac = "August", nazov = "Triatlon", mesto = "Nitra,", miesto = " Stadion", cas = "17:00" };
            udalosti.Add(udalost);
            zoznamUdalosti.ItemsSource = udalosti;
        }
    }
}