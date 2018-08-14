using System;
using System.Collections.Generic;
using Udalosti.Autentifikacia.Data;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Udalosti
{
    public sealed partial class Registracia : Page, KommunikaciaOdpoved
    {   
        public Registracia()
        {
            this.InitializeComponent();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += prihlasanie;
        }

        public async System.Threading.Tasks.Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_REGISTRACIA:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        kommunikacia("Úspech", "Registrácia prebehla úspesne! Možete sa prihlásiť.", "Ďalej", true);
                    }
                    else
                    {
                        kommunikacia("Chyba", odpoved, "Zatvoriť", false);
                    }
                    break;
            }
        }

        private async void kommunikacia(String titul, String odpoved, String tlacidlo, bool uspech)
        {
            MessageDialog dialog = new MessageDialog(odpoved);
            dialog.Title = titul;
            dialog.Commands.Add(new UICommand(tlacidlo) { Id = 0 });
            dialog.DefaultCommandIndex = 0;
            var odpovedTlacidlom = await dialog.ShowAsync();

            if ((int)odpovedTlacidlom.Id == 0)
            {
                if (uspech) {
                    registracia.Navigate(typeof(Prihlasenie), null, new DrillInNavigationTransitionInfo());
                }
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

        private async void registrovat(object sender, RoutedEventArgs e)
        {
            await new AutentifkaciaUdaje(this).registraciaAsync(meno.Text, email.Text, heslo.Password, potvrd.Password);
        }
    }
}