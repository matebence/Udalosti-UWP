using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udaje.Siet
{
    interface KommunikaciaOdpoved
    {
        Task odpovedServera(String odpoved, String od, Dictionary<String, String> udaje);
    }
}
