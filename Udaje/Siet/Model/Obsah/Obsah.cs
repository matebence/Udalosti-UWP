using System.Collections.Generic;
using Udalosti.Udalosti.Zoznam;

namespace Udalosti.Udaje.Siet.Model.Obsah
{
   public class Obsah
    {
        public Obsah(List<Udalost> udalosti)
        {
            this.udalosti = udalosti;
        }

        public List<Udalost> udalosti { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}