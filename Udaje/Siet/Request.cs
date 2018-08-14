using System.Collections.Generic;
using System.Net.Http;

namespace Udalosti.Udaje.Siet
{
    class Request
    {
        private string udalostiAdresa = "http://192.168.247.131/udalosti/index.php/";
        private HttpClient httpClient;

        public Request()
        {
            this.httpClient = new HttpClient();
        }

        public async System.Threading.Tasks.Task<HttpResponseMessage> novyPostRequestAsync(Dictionary<string, string> obsah, string adresa) {
            this.udalostiAdresa += adresa;

            var request = new FormUrlEncodedContent(obsah);
            var odpoved = await httpClient.PostAsync(udalostiAdresa, request);

            return odpoved;
        }
    }
}
