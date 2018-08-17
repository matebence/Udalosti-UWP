using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Data;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Autentifikator;
using Udalosti.Udaje.Siet.Model.Pozicia;
using System.Diagnostics;

namespace Udalosti.Autentifikacia.Data
{
    class AutentifkaciaUdaje : AutentifkaciaImplementacia
    {
        private KommunikaciaOdpoved odpovedeOdServera;
        private SQLiteDatabaza sqliteDataza;

        public AutentifkaciaUdaje(KommunikaciaOdpoved odpovedeOdServera)
        {
            this.sqliteDataza = new SQLiteDatabaza();
            this.odpovedeOdServera = odpovedeOdServera;
        }

        public async Task miestoPrihlaseniaAsync(string email, string heslo)
        {
            Debug.WriteLine("Metoda miestoPrihlaseniaAsync bola vykonana");

            HttpResponseMessage odpoved = await new Request().novyGetRequestAsync("json");
            String stat, okres, mesto;
            stat = okres = mesto = "";

            if (odpoved.IsSuccessStatusCode)
            {
                Pozicia pozicia = JsonConvert.DeserializeObject<Pozicia>(await odpoved.Content.ReadAsStringAsync());

                if (pozicia.country != null)
                {
                    stat = pozicia.country;
                }
                if (pozicia.regionName != null)
                {
                    okres = pozicia.regionName;
                }
                if (pozicia.city != null)
                {
                    mesto = pozicia.city;
                }

                if (sqliteDataza.miestoPrihlasenia())
                {
                    sqliteDataza.aktualizujMiestoPrihlasenia(new Miesto(stat, okres, mesto));
                }
                else
                {
                    sqliteDataza.noveMiestoPrihlasenia(new Miesto(stat, okres, mesto));
                }
            }
            await prihlasenieAsync(email, heslo, stat, okres, mesto);
        }

        public async Task prihlasenieAsync(string email, string heslo, string stat, string okres, string mesto)
        {
            Debug.WriteLine("Metoda prihlasenieAsync bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "heslo", heslo },
               { "pokus_o_prihlasenie", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "index.php/prihlasenie");
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (autentifikator.chyba)
                {
                    udaje.Add("email", email);
                    if (autentifikator.validacia.email != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                    else if (autentifikator.validacia.oznam != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                }
                else
                {
                    udaje.Add("email", email);
                    udaje.Add("heslo", heslo);
                    udaje.Add("token", autentifikator.pouzivatel.token);

                    await odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                }
            }
            else
            {
                await odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
            }
        }

        public async Task registraciaAsync(string meno, string email, string heslo, string potvrd)
        {
            Debug.WriteLine("Metoda registraciaAsync bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "meno", meno },
               { "email", email },
               { "heslo", heslo },
               { "potvrd", potvrd },
               { "nova_registracia", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "index.php/registracia");
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                if (autentifikator.chyba)
                {
                    if (autentifikator.validacia.oznam != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.meno != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.meno, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.email != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.potvrd != null)
                    {
                        await odpovedeOdServera.odpovedServera(autentifikator.validacia.potvrd, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                }
                else
                {
                    await odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                }
            }
            else
            {
                await odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
            }
        }

        public void ucetJeNePristupny(string email)
        {
            Debug.WriteLine("Metoda ucetJeNePristupny bola vykonana");

            sqliteDataza.odstranPouzivatelskeUdaje(email);
        }

        public void ulozPrihlasovacieUdajeDoDatabazy(string email, string heslo, string token)
        {
            Debug.WriteLine("Metoda ulozPrihlasovacieUdajeDoDatabazy bola vykonana");

            if (sqliteDataza.pouzivatelskeUdaje())
            {
                sqliteDataza.aktualizujPouzivatelskeUdaje(new Pouzivatelia(email, heslo, token));
            }
            else
            {
                sqliteDataza.novePouzivatelskeUdaje(new Pouzivatelia(email, heslo, token));
            }
        }
    }
}