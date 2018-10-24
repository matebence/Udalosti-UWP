using System;

namespace Udalosti.Udaje.Siet.Model.Akcia
{
    class Akcia
    {
        public Akcia(string uspech, string chyba)
        {
            this.uspech = uspech;
            this.chyba = chyba;
        }

        public String uspech { get; set; }
        public String chyba { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}