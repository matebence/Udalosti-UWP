using System;
using System.Threading.Tasks;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifkaciaImplementacia
    {
        Task miestoPrihlaseniaAsync(String email, String heslo, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj);

        Task miestoPrihlaseniaAsync(String email, String heslo);

        Task prihlasenieAsync(String email, String heslo);

        Task registraciaAsync(String meno, String email, String heslo, String potvrd);

        void ulozPrihlasovacieUdajeDoDatabazy(String email, String heslo, String token);

        void ucetJeNePristupny(String email);
    }
}