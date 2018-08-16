using System;
using System.Collections.Generic;

namespace Udalosti.Udalosti.Data
{
    interface UdalostiImplementacia
    {
        void zoznamUdalosti(String email, String stat, String token);

        void zoznamUdalostiPodlaPozicie(String email, String stat, String okres, String mesto, String token);

        void automatickePrihlasenieVypnute(String email);

        void odhlasenie(String email);

        Dictionary<string, string> miestoPrihlasenia();
    }
}