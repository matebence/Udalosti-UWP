using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udalosti.Zoznam;

namespace Udalosti.Udaje.Siet
{
    interface KommunikaciaData
    {
        Task dataZoServera(String odpoved, String od, List<Udalost> udaje);
    }
}
