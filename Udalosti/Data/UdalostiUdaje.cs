using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Udalosti.Udaje.Data;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Autentifikator;
using Udalosti.Udaje.Siet.Model.Akcia;
using Udalosti.Udaje.Siet.Model.Obsah;

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

        public async Task zoznamUdalosti(string email, string stat, string token)
        {
            Debug.WriteLine("Metoda zoznamUdalosti bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "stat", stat },
               { "token", token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZOZNAM_UDALOSTI);
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_OBJAVUJ, data.udalosti);
            }
            else
            {
                await this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.UDALOSTI_OBJAVUJ, null);
            }
        }

        public async Task zoznamUdalostiPodlaPozicie(string email, string stat, string okres, string mesto, string token)
        {
            Debug.WriteLine("Metoda zoznamUdalostiPodlaPozicie bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "stat", stat },
               { "okres", okres },
               { "mesto", mesto },
               { "token", token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZOZNAM_UDALOSTI_PODLA_POZCIE);
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_PODLA_POZICIE, data.udalosti);
            }
            else
            {
                await this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.UDALOSTI_PODLA_POZICIE, null);
            }
        }

        public Dictionary<string, string> miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            Dictionary<string, string> miestoPrihlasenia = this.sqliteDatabaza.vratMiestoPrihlasenia();
            if (miestoPrihlasenia != null)
            {
                return miestoPrihlasenia;
            }
            else
            {
                return null;
            }
        }

        public async Task zoznamZaujmov(string email, string token)
        {
            Debug.WriteLine("Metoda zoznamZaujmov bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "token", token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZOZNAM_ZAUJMOV);
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ZOZNAM, data.udalosti);
            }
            else
            {
                await this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_ZOZNAM, null);
            }
        }

        public async Task zaujem(string email, string token, int idUdalost)
        {
            Debug.WriteLine("Metoda zaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "token", token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZAUJEM);
            if (odpoved.IsSuccessStatusCode)
            {
                Dictionary<string, string> udaje = new Dictionary<string, string>();
                Akcia data = JsonConvert.DeserializeObject<Akcia>(await odpoved.Content.ReadAsStringAsync());

                if (data.uspech != null)
                {
                    udaje.Add("uspech", data.uspech);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM, udaje);
                }

                if (data.chyba != null)
                {
                    udaje.Add("chyba", data.chyba);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM, udaje);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.ZAUJEM, null);
            }
        }

        public async Task potvrdZaujem(string email, string token, int idUdalost)
        {
            Debug.WriteLine("Metoda potvrdZaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "token", token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_POTVRD_ZAUJEM);
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_POTVRD, data.udalosti);
            }
            else
            {
                await this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_POTVRD, null);
            }
        }

        public async Task odstranZaujem(string email, string token, int idUdalost)
        {
            Debug.WriteLine("Metoda odstranZaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email },
               { "token", token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ODSTRAN_ZAUJEM);
            if (odpoved.IsSuccessStatusCode)
            {
                Dictionary<string, string> udaje = new Dictionary<string, string>();
                Akcia data = JsonConvert.DeserializeObject<Akcia>(await odpoved.Content.ReadAsStringAsync());

                if (data.uspech != null)
                {
                    udaje.Add("uspech", data.uspech);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ODSTRANENIE, udaje);
                }

                if (data.chyba != null)
                {
                    udaje.Add("chyba", data.chyba);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ODSTRANENIE, udaje);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_ODSTRANENIE, null);
            }
        }

        public void automatickePrihlasenieVypnute(string email)
        {
            Debug.WriteLine("Metoda automatickePrihlasenieVypnute bola vykonana");

            this.sqliteDatabaza.odstranPouzivatelskeUdaje(email);
        }

        public async Task odhlasenie(string email, bool async)
        {
            Debug.WriteLine("Metoda odhlasenie bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ODHLASENIE);
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (!autentifikator.chyba)
                {
                    if (async)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                    }
                    else
                    {
                        this.odpovedeOdServera.odpovedServer(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                    }
                }
            }
            else
            {
                if (async)
                {
                    await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                }
                else
                {
                    this.odpovedeOdServera.odpovedServer("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                }
            }
        }

        public async Task<bool> obrazokJeDostupnny(string adresa, bool podrobnosti)
        {
            Debug.WriteLine("Metoda obrazokJeDostupnny bola vykonana");

            WebRequest request;
            if (podrobnosti)
            {
                request = WebRequest.Create(adresa);
            }
            else
            {
                request = WebRequest.Create(App.udalostiAdresa + adresa);
            }

            WebResponse odpoved;
            try
            {
                odpoved = await request.GetResponseAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}