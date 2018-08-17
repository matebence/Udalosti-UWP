using System.Collections;

namespace Udalosti.Udaje.Siet.Model.Obsah
{
    class Obsah
    {
        public Obsah(ArrayList udalosti)
        {
            this.udalosti = udalosti;
        }

        public ArrayList udalosti { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
