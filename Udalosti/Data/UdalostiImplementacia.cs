using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udalosti.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalostiAsync(String email, String stat, String token);

        Task zoznamUdalostiPodlaPozicieAsync(String email, String stat, String okres, String mesto, String token);

        Dictionary<string, string> miestoPrihlasenia();

        void automatickePrihlasenieVypnute(String email);

        Task odhlasenieAsync(String email);

        Task zoznamZaujmovAsync(String email, String token);

        Task zaujemAsync(String email, String token, int idUdalost);

        Task potvrdZaujemAsync(String email, String token, int idUdalost);

        Task odstranZaujemAsync(String email, String token, int idUdalost);

        Task<bool> obrazokJeDostupnnyAsync(string adresa, bool podrobnosti);
    }
}