using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Udalosti.Dialog
{
    class DialogOznameni
    {
        public static async Task kommunikaciaAsync(string titul, string odpoved, string tlacidlo, bool uspech, Frame frame)
        {
            Debug.WriteLine("Metoda kommunikaciaAsync bola vykonana");

            MessageDialog dialog = new MessageDialog(odpoved);

            dialog.Title = titul;
            dialog.Commands.Add(new UICommand(tlacidlo) { Id = 0 });
            dialog.DefaultCommandIndex = 0;

            var odpovedTlacidlom = await dialog.ShowAsync();

            if ((int)odpovedTlacidlom.Id == 0)
            {
                if (uspech && frame != null)
                {
                    frame.GoBack();
                }
            }
        }
    }
}