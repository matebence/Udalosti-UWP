using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Pouzivatel
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int idPouzivatel { get; set; }
        public String email { get; set; }
        public String heslo { get; set; }

        public Pouzivatel()
        {
        }

        public Pouzivatel(string email, string heslo)
        {
            this.email = email;
            this.heslo = heslo;
        }
    }
}
