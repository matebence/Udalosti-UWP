using System;
using System.IO;
using Udalosti.Udaje.Data;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Udalosti
{
    sealed partial class App : Application
    {
        public static string databaza = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "udalosti.sqlite"));
        public static string udalostiAdresa = "http://192.168.247.131/udalosti/index.php/";
        public static string geoAdresa = "http://ip-api.com/";

        public App()
        {
            this.InitializeComponent();
            this.prvyStart();

            this.Suspending += OnSuspending;
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
                    rootFrame.Navigate(typeof(Prihlasenie), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        private void prvyStart()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("prvyStart"))
            {
                if (!(bool)ApplicationData.Current.LocalSettings.Values["prvyStart"])
                {
                    SQLiteDatabaza sqliteDatabaza = new SQLiteDatabaza();
                    sqliteDatabaza.VyvorDatabazu();
                }
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values["prvyStart"] = false;
            }
        }
    }
}