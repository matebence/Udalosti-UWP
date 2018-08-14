using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Udalosti.Autentifikacia.Data
{
    class AutentifkaciaUdaje : AutentifkaciaImplementacia
    {
        public void miestoPrihlasenia(string email, string heslo)
        {
        }

        public void prihlasenie(string email, string heslo, string stat, string okres, string mesto)
        {
        }

        public async Task registraciaAsync(string meno, string email, string heslo, string potvrd)
        {
        HttpClient client = new HttpClient();
        var values = new Dictionary<string, string>
            {
               { "nova_registracia", "hello" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://192.168.247.131/udalosti/index.php/registracia", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(responseString);
        }

        public void ucetJeNePristupny(string email)
        {
        }

        public void ulozPrihlasovacieUdajeDoDatabazy(string email, string heslo)
        {
        }

    }
}
