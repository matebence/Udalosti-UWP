using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Udalosti
{
    public sealed partial class Registracia : Page, KommunikaciaOdpoved
    {
        public Registracia()
        {
            this.InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += prihlasanie;
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            nacitavanie.IsActive = false;
            nacitavanie.Visibility = Visibility.Collapsed;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_REGISTRACIA:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        await DialogOznameni.kommunikaciaAsync("Úspech", "Registrácia prebehla úspesne! Možete sa prihlásiť.", "Ďalej", true, registracia);
                    }
                    else
                    {
                        await DialogOznameni.kommunikaciaAsync("Chyba", odpoved, "Zatvoriť", false, registracia);
                    }
                    break;
            }
        }

        private void prihlasanie(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame registracia = Window.Current.Content as Frame;
            if (registracia == null)
                return;
            if (registracia.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                registracia.GoBack();
            }
        }

        private async void registrovatAsync(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                await new AutentifkaciaUdaje(this).registraciaAsync(meno.Text, email.Text, heslo.Password, potvrd.Password);
            }
            else
            {
                await DialogOznameni.kommunikaciaAsync("Chyba", "Žiadné spojenie!", "Zatvoriť", false, registracia);
            }
        }
    }
}