using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Model.Obsah;
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
        public static string udalostiAdresa = "https://bmate18.student.ki.fpv.ukf.sk/udalosti/";
        public static string geoAdresa = "http://ip-api.com/";

        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        public App()
        {
            this.InitializeComponent();

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);

            this.uvodnaObrazovkaUdaje.prvyStart();
            this.Suspending += OnSuspendingAsync;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
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
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private async void OnSuspendingAsync(object sender, SuspendingEventArgs e)
        {
            await this.udalostiUdaje.odhlasenieAsync(this.uvodnaObrazovkaUdaje.prihlasPouzivatela()["email"]);
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
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

        public Task dataZoServeraAsync(string odpoved, string od, List<Udalost> udaje)
        {
            throw new NotImplementedException();
        }
    }
}