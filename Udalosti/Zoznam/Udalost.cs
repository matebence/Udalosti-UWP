namespace Udalosti.Udalosti.Zoznam
{
   public class Udalost
    {
        public Udalost(int idUdalost, string obrazok, string den, string mesiac, string nazov, string mesto, string miesto, string cas)
        {
            this.idUdalost = idUdalost;
            this.obrazok = obrazok;
            this.den = den;
            this.mesiac = mesiac;
            this.nazov = nazov;
            this.mesto = mesto;
            this.miesto = miesto;
            this.cas = cas;
        }

        public int idUdalost { get; set; }
        public string obrazok { get; set; }
        public string den { get; set; }
        public string mesiac { get; set; }
        public string nazov { get; set; }
        public string mesto { get; set; }
        public string miesto { get; set; }
        public string cas { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
