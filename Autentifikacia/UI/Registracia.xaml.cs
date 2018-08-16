﻿using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace Udalosti
{
    public sealed partial class Registracia : Page, KommunikaciaOdpoved
    {
        private AutentifkaciaUdaje autentifkaciaUdaje;

        public Registracia()
        {
            this.InitializeComponent();
            this.autentifkaciaUdaje = new AutentifkaciaUdaje(this);
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

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            SystemNavigationManager.GetForCurrentView().BackRequested += spatNaPrihlasanie;
            base.OnNavigatedTo(e);
        }

        private void spatNaPrihlasanie(object sender, BackRequestedEventArgs e)
        {
            Debug.WriteLine("Metoda spatNaPrihlasanie bola vykonana");

            Frame registracia = Window.Current.Content as Frame;
            if (registracia.CanGoBack)
            {
                e.Handled = true;
                registracia.GoBack();
            }
        }

        private async void registrovatAsync(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Metoda registrovatAsync bola vykonana");

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                nacitavanie.IsActive = true;
                nacitavanie.Visibility = Visibility.Visible;

                await autentifkaciaUdaje.registraciaAsync(meno.Text, email.Text, heslo.Password, potvrd.Password);
            }
            else
            {
                await DialogOznameni.kommunikaciaAsync("Chyba", "Žiadné spojenie!", "Zatvoriť", false, registracia);
            }
        }
    }
}