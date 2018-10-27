using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Udalosti.Udaje.Siet
{
    class Request
    {
        private HttpClient httpClient;

        public Request()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> postRequestUdalostiServer(Dictionary<string, string> obsah, string adresa)
        {
            Debug.WriteLine("Metoda novyPostRequestAsync bola vykonana");

            adresa = App.udalostiAdresa + adresa;

            var request = new FormUrlEncodedContent(obsah);
            var odpoved = await this.httpClient.PostAsync(adresa, request);

            return odpoved;
        }

        public async Task<HttpResponseMessage> getRequestGeoServer(string adresa)
        {
            Debug.WriteLine("Metoda novyGetRequestAsync - ipAdresa bola vykonana");

            adresa = App.ipAdresa + adresa;
            return await this.httpClient.GetAsync(adresa);
        }

        public async Task<HttpResponseMessage> getRequestLocationServer(double lat, double lon)
        {
            Debug.WriteLine("Metoda novyGetRequestAsync - geoAdresa bola vykonana");

            string adresa = App.geoAdresa + "&lat="+lat+ "&lon="+lon+ "&format="+Nastavenia.Nastavenia.POZICIA_FORMAT + "&accept-language="+Nastavenia.Nastavenia.POZICIA_JAZYK;
            return await this.httpClient.GetAsync(adresa);
        }
    }
}