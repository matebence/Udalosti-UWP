using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udalosti.Zoznam;

namespace Udalosti.Udaje.Siet
{
    interface KommunikaciaData
    {
        Task dataZoServera(string odpoved, string od, List<Udalost> udaje);
    }
}
