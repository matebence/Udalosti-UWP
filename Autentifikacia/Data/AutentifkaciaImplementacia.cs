using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifkaciaImplementacia
    {
        void ucetJeNePristupny(String email);

        void ulozPrihlasovacieUdajeDoDatabazy(String email, String heslo);

        Task miestoPrihlaseniaAsync(String email, String heslo);

        Task prihlasenieAsync(String email, String heslo, String stat, String okres, String mesto);

        Task registraciaAsync(String meno, String email, String heslo, String potvrd);
    }
}