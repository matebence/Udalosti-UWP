namespace Udalosti.Udaje.Siet.Autentifikator
{
    class Autentifikator
    {
        public Autentifikator(bool chyba, Pouzivatel pouzivatel, Validacia validacia)
        {
            this.chyba = chyba;
            this.pouzivatel = pouzivatel;
            this.validacia = validacia;
        }

        public bool chyba { get; set; }
        public Pouzivatel pouzivatel { get; set; }
        public Validacia validacia { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
