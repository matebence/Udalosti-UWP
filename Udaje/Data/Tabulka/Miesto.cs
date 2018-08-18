using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Miesto
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int idMiesto { get; set; }
        public String stat { get; set; }
        public String okres { get; set; }
        public String mesto { get; set; }

        public Miesto()
        {
        }

        public Miesto(string stat, string okres, string mesto)
        {
            this.stat = stat;
            this.okres = okres;
            this.mesto = mesto;
        }
    }
}