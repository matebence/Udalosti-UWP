using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Udalosti.Udaje.Data;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Autentifikator;

namespace Udalosti.Udalosti.Data
{
    class UdalostiUdaje : UdalostiImplementacia
    {
        private KommunikaciaOdpoved odpovedeOdServera;
        private KommunikaciaData udajeZoServera;
        private SQLiteDatabaza sqliteDatabaza;

        public UdalostiUdaje(KommunikaciaOdpoved odpovedeOdServera, KommunikaciaData udajeZoServera)
        {
            this.odpovedeOdServera = odpovedeOdServera;
            this.udajeZoServera = udajeZoServera;
            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        public void automatickePrihlasenieVypnute(string email)
        {
            Debug.WriteLine("Metoda automatickePrihlasenieVypnute bola vykonana");

            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            throw new System.NotImplementedException();
        }

        public async Task odhlasenieAsync(string email)
        {
            Debug.WriteLine("Metoda odhlasenieAsync bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email }
            };

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "prihlasenie/odhlasit_sa");
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (!autentifikator.chyba)
                {
                await odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                }
            }
            else
            {
                await odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
            }
        }

        public void zoznamUdalosti(string email, string stat, string token)
        {
            Debug.WriteLine("Metoda zoznamUdalosti bola vykonana");

            throw new System.NotImplementedException();
        }

        public void zoznamUdalostiPodlaPozicie(string email, string stat, string okres, string mesto, string token)
        {
            Debug.WriteLine("Metoda zoznamUdalostiPodlaPozicie bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}