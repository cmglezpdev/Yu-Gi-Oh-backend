using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                string url = "https://db.ygoprodeck.com/api/v7/cardinfo.php";

                client.DefaultRequestHeaders.Clear();

                var response = client.GetAsync(url).Result;

                var res = response.Content.ReadAsStringAsync().Result;
                dynamic r = JObject.Parse(res);

                System.Console.WriteLine(r);
            }
        }
    }
}