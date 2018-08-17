using System;
using System.Collections;
using System.Threading.Tasks;

namespace Udalosti.Udaje.Siet
{
    interface KommunikaciaData
    {
        Task dataZoServeraAsync(String odpoved, String od, ArrayList udaje);
    }
}
