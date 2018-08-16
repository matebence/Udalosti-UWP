using System.Collections.Generic;

namespace Udalosti.Uvod.Data
{
    interface UvodnaObrazovkaImplementacia
    {
        void prvyStart();

        bool zistiCiPouzivatelskoKontoExistuje();

        Dictionary<string, string> prihlasPouzivatela();
    }
}
