using System;
using System.Threading.Tasks;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifkaciaImplementacia
    {
        Task miestoPrihlasenia(string email, string heslo, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj, bool async);

        Task miestoPrihlasenia(string email, string heslo, bool async);

        Task prihlasenie(string email, string heslo, bool async);

        Task registracia(string meno, string email, string heslo, string potvrd);

        void ulozPrihlasovacieUdajeDoDatabazy(string email, string heslo, string token);

        void ucetJeNePristupny(string email);
    }
}