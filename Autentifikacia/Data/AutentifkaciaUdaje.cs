using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Udalosti.Udaje.Data;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Autentifikator;
using Udalosti.Udaje.Siet.Model.Pozicia;

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
        
        public async Task miestoPrihlasenia(string email, string heslo, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj, bool async)
        {
            Debug.WriteLine("Metoda miestoPrihlasenia - GEO bola vykonana");

            HttpResponseMessage odpoved = await new Request().getRequestLocationServer(zemepisnaSirka, zemepisnaDlzka);
            string pozicia, okres, kraj, psc, stat, znakStatu;
            pozicia = okres = kraj = psc = stat = znakStatu = "";

            if (odpoved.IsSuccessStatusCode)
            {
                LocationIQ locationIQ = JsonConvert.DeserializeObject<LocationIQ>(await odpoved.Content.ReadAsStringAsync());

                if (locationIQ.address != null)
                {
                    if (locationIQ.address.city_district != null)
                    {
                        pozicia = locationIQ.address.city_district;
                    }
                    else
                    {
                        pozicia = "pozicia neurcena";
                    }
                    if (locationIQ.address.city != null)
                    {
                        okres = locationIQ.address.city;
                    }
                    else
                    {
                        okres = "okres neurcena";
                    }
                    if (locationIQ.address.state != null)
                    {
                        kraj = locationIQ.address.state;
                    }
                    else
                    {
                        kraj = "kraj neurcena";
                    }
                    if (locationIQ.address.postcode != null)
                    {
                        psc = locationIQ.address.postcode;
                    }
                    else
                    {
                        psc = "psc neurcena";
                    }
                    if (locationIQ.address.country != null)
                    {
                        stat = locationIQ.address.country;
                    }
                    else
                    {
                        stat = "stat neurcena";
                    }
                    if (locationIQ.address.country_code != null)
                    {
                        znakStatu = locationIQ.address.country_code;
                    }
                    else
                    {
                        znakStatu = "znakStatu neurcena";
                    }

                    if (sqliteDataza.miestoPrihlasenia())
                    {
                        sqliteDataza.aktualizujMiestoPrihlasenia(new Miesto(pozicia, okres, kraj, psc, stat, znakStatu), 1);
                    }
                    else
                    {
                        sqliteDataza.noveMiestoPrihlasenia(new Miesto(pozicia, okres, kraj, psc, stat, znakStatu));
                    }
                }

                if (aktualizuj)
                {
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_AKTUALIZUJ, null);
                }
                else
                {
                    await prihlasenie(email, heslo, async);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
            }
        }

        public async Task miestoPrihlasenia(string email, string heslo, bool async)
        {
            Debug.WriteLine("Metoda miestoPrihlasenia - IP bola vykonana");

            HttpResponseMessage odpoved = await new Request().getRequestGeoServer(Nastavenia.SERVER_GEO_IP);

            if (odpoved.IsSuccessStatusCode)
            {
                Pozicia pozicia = JsonConvert.DeserializeObject<Pozicia>(await odpoved.Content.ReadAsStringAsync());
                if (sqliteDataza.miestoPrihlasenia())
                {
                    sqliteDataza.aktualizujMiestoPrihlasenia(new Miesto(null, null, null, null, pozicia.country, null), 1);
                }
                else
                {
                    sqliteDataza.noveMiestoPrihlasenia(new Miesto(null, null, null, null, pozicia.country, null));
                }
                await prihlasenie(email, heslo, async);
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
            }
        }

        public async Task prihlasenie(string email, string heslo, bool async)
        {
            Debug.WriteLine("Metoda prihlasenie bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "heslo", heslo },
               { "pokus_o_prihlasenie", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_PRIHLASENIE);
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (autentifikator.chyba)
                {
                    udaje.Add("email", email);
                    if (autentifikator.validacia.email != null)
                    {
                        if (async)
                        {
                            await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                        }
                        else
                        {
                            this.odpovedeOdServera.odpovedServer(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                        }
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        if (async)
                        {
                            await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                        }
                        else
                        {
                            this.odpovedeOdServera.odpovedServer(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                        }
                    }
                    else if (autentifikator.validacia.oznam != null)
                    {
                        if (async)
                        {
                            await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                        }
                        else
                        {
                            this.odpovedeOdServera.odpovedServer(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                        }
                    }
                }
                else
                {
                    udaje.Add("email", email);
                    udaje.Add("heslo", heslo);
                    udaje.Add("token", autentifikator.pouzivatel.token);

                    if (async)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                    else
                    {
                        this.odpovedeOdServera.odpovedServer(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                }
            }
            else
            {
                if (async)
                {
                    await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
                }
                else
                {
                    this.odpovedeOdServera.odpovedServer("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
                }
            }
        }

        public async Task registracia(string meno, string email, string heslo, string potvrd)
        {
            Debug.WriteLine("Metoda registracia bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "meno", meno },
               { "email", email },
               { "heslo", heslo },
               { "potvrd", potvrd },
               { "nova_registracia", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_REGISTRACIA);
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                if (autentifikator.chyba)
                {
                    if (autentifikator.validacia.oznam != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.meno != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.meno, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.email != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.potvrd != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.potvrd, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                }
                else
                {
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
            }
        }

        public void ulozPrihlasovacieUdajeDoDatabazy(string email, string heslo, string token)
        {
            Debug.WriteLine("Metoda ulozPrihlasovacieUdajeDoDatabazy bola vykonana");

            if (this.sqliteDataza.pouzivatelskeUdaje())
            {
                this.sqliteDataza.aktualizujPouzivatelskeUdaje(new Pouzivatelia(email, heslo, token), email);
            }
            else
            {
                this.sqliteDataza.novePouzivatelskeUdaje(new Pouzivatelia(email, heslo, token));
            }
        }

        public void ucetJeNePristupny(string email)
        {
            Debug.WriteLine("Metoda ucetJeNePristupny bola vykonana");

            this.sqliteDataza.odstranPouzivatelskeUdaje(email);
        }
    }
}