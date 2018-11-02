using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Pouzivatelia
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int idPouzivatel { get; set; }
        public string email { get; set; }
        public string heslo { get; set; }
        public string token { get; set; }

        public Pouzivatelia()
        {
        }

        public Pouzivatelia(string email, string heslo, string token)
        {
            this.email = email;
            this.heslo = heslo;
            this.token = token;
        }
    }
}
