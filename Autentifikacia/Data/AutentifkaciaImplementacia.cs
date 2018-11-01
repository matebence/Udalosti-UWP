using System;
using System.Threading.Tasks;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifkaciaImplementacia
    {
        Task miestoPrihlasenia(String email, String heslo, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj, bool async);

        Task miestoPrihlasenia(String email, String heslo, bool async);

        Task prihlasenie(String email, String heslo, bool async);

        Task registracia(String meno, String email, String heslo, String potvrd);

        void ulozPrihlasovacieUdajeDoDatabazy(String email, String heslo, String token);

        void ucetJeNePristupny(String email);
    }
}