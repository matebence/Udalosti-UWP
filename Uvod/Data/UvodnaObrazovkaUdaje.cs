using System.Collections.Generic;
using System.Diagnostics;
using Udalosti.Udaje.Data;
using Windows.Storage;

namespace Udalosti.Uvod.Data
{
    class UvodnaObrazovkaUdaje : UvodnaObrazovkaImplementacia
    {
        private SQLiteDatabaza sqliteDatabaza;

        public UvodnaObrazovkaUdaje()
        {
            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        public Dictionary<string, string> prihlasPouzivatela()
        {
            Debug.WriteLine("Metoda prihlasPouzivatela bola vykonana");

            return sqliteDatabaza.vratAktualnehoPouzivatela();
        }

        public void prvyStart()
        {
            Debug.WriteLine("Metoda prvyStart bola vykonana");

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("prvyStart"))
            {
                ApplicationData.Current.LocalSettings.Values["prvyStart"] = false;
                sqliteDatabaza.VyvorDatabazu();
            }
        }

        public bool zistiCiPouzivatelskoKontoExistuje()
        {
            Debug.WriteLine("Metoda zistiCiPouzivatelskoKontoExistuje bola vykonana");

            return sqliteDatabaza.pouzivatelskeUdaje();
        }
    }
}