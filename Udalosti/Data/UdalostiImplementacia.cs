using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udalosti.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalostiAsync(String email, String stat, String token);

        void zoznamUdalostiPodlaPozicie(String email, String stat, String okres, String mesto, String token);

        void automatickePrihlasenieVypnute(String email);

        Task odhlasenieAsync(String email);

        Dictionary<string, string> miestoPrihlasenia();
    }
}