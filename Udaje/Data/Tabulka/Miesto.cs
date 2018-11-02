using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Miesto
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int idMiesto { get; set; }
        public string pozicia { get; set; }
        public string okres { get; set; }
        public string kraj { get; set; }
        public string psc { get; set; }
        public string stat { get; set; }
        public string znakStatu { get; set; }
        
        public Miesto()
        {
        }

        public Miesto(string pozicia, string okres, string kraj, string psc, string stat, string znakStatu)
        {
            this.pozicia = pozicia;
            this.okres = okres;
            this.kraj = kraj;
            this.psc = psc;
            this.stat = stat;
            this.znakStatu = znakStatu;
        }
    }
}