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
    public sealed partial class Prihlasenie : Page
    {
        public Prihlasenie()
        {
            this.InitializeComponent();
        }

        private void tlacidloRegistrovatSa(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracia));
        }

        private void prihlasit(object sender, RoutedEventArgs e)
        {

        }
    }
}
