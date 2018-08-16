using System.Collections.Generic;
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
            return sqliteDatabaza.vratAktualnehoPouzivatela();
        }

        public void prvyStart()
        {
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("prvyStart"))
            {
                ApplicationData.Current.LocalSettings.Values["prvyStart"] = false;
                sqliteDatabaza.VyvorDatabazu();
            }
        }

        public bool zistiCiPouzivatelskoKontoExistuje()
        {
            return sqliteDatabaza.pouzivatelskeUdaje();
        }
    }
}