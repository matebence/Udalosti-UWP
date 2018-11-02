using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udaje.Siet
{
    interface KommunikaciaOdpoved
    {
        Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje);

        void odpovedServer(string odpoved, string od, Dictionary<string, string> udaje);
    }
}
