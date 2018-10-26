using System.Diagnostics;

namespace Udalosti.Udalosti.SpracovanieDat
{
    class AktualizatorObsahu
    {
        private static AktualizatorObsahu aktualizatorObsahu;
        private Aktualizator aktualizator;

        private AktualizatorObsahu() { }

        public static AktualizatorObsahu zaujmy()
        {
            Debug.WriteLine("Metoda AktualizatorObsahu bola vykonana");

            if (AktualizatorObsahu.aktualizatorObsahu == null)
            {
                AktualizatorObsahu.aktualizatorObsahu = new AktualizatorObsahu();
            }
            return AktualizatorObsahu.aktualizatorObsahu;
        }

        public void nastav(Aktualizator aktualizator)
        {
            Debug.WriteLine("Metoda nastav bola vykonana");

            this.aktualizator = aktualizator;
        }

        private void aktualizuj()
        {
            Debug.WriteLine("Metoda aktualizuj bola vykonana");

            this.aktualizator.aktualizujObsahZaujmov();
        }

        public void hodnota()
        {
            Debug.WriteLine("Metoda hodnota bola vykonana");

            if (this.aktualizator != null)
            {
                aktualizuj();
            }
        }
    }
}
