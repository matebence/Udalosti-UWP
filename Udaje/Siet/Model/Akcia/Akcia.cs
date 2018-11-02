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

        public string uspech { get; set; }
        public string chyba { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}