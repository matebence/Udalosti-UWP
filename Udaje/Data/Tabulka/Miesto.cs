using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Miesto
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int idMiesto { get; set; }
        public String pozicia { get; set; }
        public String okres { get; set; }
        public String kraj { get; set; }
        public String psc { get; set; }
        public String stat { get; set; }
        public String znakStatu { get; set; }
        
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