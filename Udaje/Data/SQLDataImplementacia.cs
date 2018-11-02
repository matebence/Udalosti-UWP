using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    interface SQLDataImplementacia
    {
        void VyvorDatabazu();

        Task<bool> tabulkaExistuje(string fileName);

        void novePouzivatelskeUdaje(Pouzivatelia pouzivatelia);

        void aktualizujPouzivatelskeUdaje(Pouzivatelia pouzivatelia, string email);

        void odstranPouzivatelskeUdaje(string email);

        bool pouzivatelskeUdaje();

        Dictionary<string, string> vratAktualnehoPouzivatela();

        void noveMiestoPrihlasenia(Miesto miesto);

        void aktualizujMiestoPrihlasenia(Miesto miesto, int idMiesto);

        void odstranMiestoPrihlasenia(int idMiesto);

        bool miestoPrihlasenia();

        Dictionary<string, string> vratMiestoPrihlasenia();
    }
}