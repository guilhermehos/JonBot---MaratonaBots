using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace JonBot.Model
{
    public class LuisModel
    {
        public string query { get; set; }
        public TopScoringIntent topScoringIntent { get; set; }

        public class TopScoringIntent
        {
            public string intent { get; set; }
            public double score { get; set; }
        }

        public async Task<LuisModel> Get(string texto)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetAsync("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/deccc51a-cb94-462c-af72-454d4b0b02c3?subscription-key=b65947689ec645dcbf7a3ffe543ce32f&verbose=true&timezoneOffset=-180&q=" + texto);

            return (await json.Content.ReadAsAsync<LuisModel>()) ?? new LuisModel();
        }
    }
}