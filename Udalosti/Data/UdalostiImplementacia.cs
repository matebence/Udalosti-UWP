using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udalosti.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalosti(String email, String stat, String token);

        Task zoznamUdalostiPodlaPozicie(String email, String stat, String okres, String mesto, String token);

        Dictionary<string, string> miestoPrihlasenia();

        void automatickePrihlasenieVypnute(String email);

        Task odhlasenie(String email, bool async);

        Task zoznamZaujmov(String email, String token);

        Task zaujem(String email, String token, int idUdalost);

        Task potvrdZaujem(String email, String token, int idUdalost);

        Task odstranZaujem(String email, String token, int idUdalost);

        Task<bool> obrazokJeDostupnny(string adresa, bool podrobnosti);
    }
}