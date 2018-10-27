using System;
using System.Diagnostics;
using Udalosti.Udalosti.Zoznam;
using Windows.UI.Xaml.Controls;

namespace Udalosti.Dialog
{
    class DialogPotvrdeni
    {
        private static DialogOdpoved dialogOdpoved;

        public static async void potvrd(string titul, string text,
            string tlacidloA, string tlacidloB,
            DialogOdpoved dialogOdpoved, Udalost udalost)
        {
            Debug.WriteLine("Metoda DialogOdpoved -potvrd bola vykonana");

            DialogPotvrdeni.dialogOdpoved = dialogOdpoved;

            ContentDialog dialog = new ContentDialog
            {
                Title = titul,
                Content = text,
                PrimaryButtonText = tlacidloA,
                CloseButtonText = tlacidloB
            };

            ContentDialogResult odpoved = await dialog.ShowAsync();
            if (odpoved == ContentDialogResult.Primary)
            {
                DialogPotvrdeni.dialogOdpoved.tlacidloA(udalost);
            }
            else
            {
                DialogPotvrdeni.dialogOdpoved.tlacidloB(udalost);
            }
        }
    }
}