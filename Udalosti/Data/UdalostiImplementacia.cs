using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udalosti.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalosti(string email, string stat, string token);

        Task zoznamUdalostiPodlaPozicie(string email, string stat, string okres, string mesto, string token);

        Dictionary<string, string> miestoPrihlasenia();

        void automatickePrihlasenieVypnute(string email);

        Task odhlasenie(string email, bool async);

        Task zoznamZaujmov(string email, string token);

        Task zaujem(string email, string token, int idUdalost);

        Task potvrdZaujem(string email, string token, int idUdalost);

        Task odstranZaujem(string email, string token, int idUdalost);

        Task<bool> obrazokJeDostupnny(string adresa, bool podrobnosti);
    }
}