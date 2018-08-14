using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Autentifikator;

namespace Udalosti.Autentifikacia.Data
{
    class AutentifkaciaUdaje : AutentifkaciaImplementacia
    {
        private KommunikaciaOdpoved odpovedeOdServera;

        public AutentifkaciaUdaje(KommunikaciaOdpoved odpovedeOdServera)
        {
            this.odpovedeOdServera = odpovedeOdServera;
        }

        public void miestoPrihlasenia(string email, string heslo)
        {
        }

        public void prihlasenie(string email, string heslo, string stat, string okres, string mesto)
        {
        }

        public async Task registraciaAsync(string meno, string email, string heslo, string potvrd)
        {
            var obsah = new Dictionary<string, string>
            {
               { "meno", meno },
               { "email", email },
               { "heslo", heslo },
               { "potvrd", potvrd },
               { "nova_registracia", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "registracia");
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                if (autentifikator.chyba)
                {
                    if (autentifikator.validacia.oznam != null)
                    {
                        odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.meno != null)
                    {
                        odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.meno, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.email != null)
                    {
                        odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);               
                    }
                    else if (autentifikator.validacia.potvrd != null)
                    {
                        odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.potvrd, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                }
                else
                {
                    odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                }
            }
            else
            {
                odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!",Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
            }
        }

        public void ucetJeNePristupny(string email)
        {
        }

        public void ulozPrihlasovacieUdajeDoDatabazy(string email, string heslo)
        {
        }

    }
}
