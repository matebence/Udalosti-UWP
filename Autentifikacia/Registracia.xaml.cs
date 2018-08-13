using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Udalosti
{
    public sealed partial class Registracia : Page
    {
        public Registracia()
        {
            this.InitializeComponent();

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += prihlasanie;
        }

        private void registrovat(object sender, RoutedEventArgs e)
        {

        }

        private void prihlasanie(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame prihlasenie = Window.Current.Content as Frame;
            if (prihlasenie == null)
                return;
            if (prihlasenie.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                prihlasenie.GoBack();
            }
        }
    }
}
