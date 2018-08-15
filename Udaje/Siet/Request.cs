using System.Collections.Generic;
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

        public async Task<HttpResponseMessage> novyPostRequestAsync(Dictionary<string, string> obsah, string adresa)
        {
            adresa = App.udalostiAdresa + adresa;

            var request = new FormUrlEncodedContent(obsah);
            var odpoved = await httpClient.PostAsync(adresa, request);

            return odpoved;
        }

        public async Task<HttpResponseMessage> novyGetRequestAsync(string adresa)
        {
            adresa = App.geoAdresa + adresa;
            return await httpClient.GetAsync(adresa);
        }
    }
}
