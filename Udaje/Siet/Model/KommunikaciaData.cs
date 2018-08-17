using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udaje.Siet.Model.Obsah;
using Udalosti.Udalosti.Zoznam;

namespace Udalosti.Udaje.Siet
{
    interface KommunikaciaData
    {
        Task dataZoServeraAsync(String odpoved, String od, List<Udalost> udaje);
    }
}
