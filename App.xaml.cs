using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udalosti.Data;
using Udalosti.Udalosti.Zoznam;
using Udalosti.Uvod.Data;
using Udalosti.Uvod.UI;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Udalosti
{
    sealed partial class App : Application, KommunikaciaOdpoved, KommunikaciaData
    {
        public static string databaza = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "udalosti.sqlite"));

        public static string udalostiAdresa = "http://app-udalosti.8u.cz/";
        public static string ipAdresa = "http://ip-api.com/";
        public static string geoAdresa = "https://eu1.locationiq.com/v1/reverse.php?key=" + Nastavenia.POZICIA_TOKEN;

        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        public App()
        {
            Debug.WriteLine("Metoda App bola vykonana");

            this.InitializeComponent();
            this.init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda App - init bola vykonana");

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);

            this.uvodnaObrazovkaUdaje.prvyStart();
            this.Suspending += OnSuspendingAsync;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Debug.WriteLine("Metoda App - OnLaunched bola vykonana");

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if (this.uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje())
                    {
                        rootFrame.Navigate(typeof(UvodnaObrazovka), e.Arguments, new DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        rootFrame.Navigate(typeof(Prihlasenie), e.Arguments, new DrillInNavigationTransitionInfo());
                    }
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Debug.WriteLine("Metoda App - OnNavigationFailed bola vykonana");

            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private async void OnSuspendingAsync(object sender, SuspendingEventArgs e)
        {
            Debug.WriteLine("Metoda App - OnSuspendingAsync bola vykonana");

            if (this.uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje())
            {
                try
                {
                    await this.udalostiUdaje.odhlasenie(this.uvodnaObrazovkaUdaje.prihlasPouzivatela()["email"], false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("CHYBA: " + ex.Message);
                }
            }
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        public void odpovedServer(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda App - odpovedServer bola vykonana");

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Systém odhlásil");
                    }
                    break;
            }
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda App - odpovedServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        public Task dataZoServera(string odpoved, string od, List<Udalost> udaje)
        {
            Debug.WriteLine("Metoda App - dataZoServera bola vykonana");

            throw new NotImplementedException();
        }
    }
}