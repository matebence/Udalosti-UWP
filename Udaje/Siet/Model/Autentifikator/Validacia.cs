using System;

namespace Udalosti.Udaje.Siet.Autentifikator
{
    class Validacia
    {
        public Validacia(string oznam, string meno, string email, string heslo, string potvrd)
        {
            this.oznam = oznam;
            this.meno = meno;
            this.email = email;
            this.heslo = heslo;
            this.potvrd = potvrd;
        }

        public String oznam { get; set; }
        public String meno { get; set; }
        public String email { get; set; }
        public String heslo { get; set; }
        public String potvrd { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
