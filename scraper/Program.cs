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
                var originalJson = JObject.Parse(res);

                var listCardJson = new List<JObject>();

                foreach(var card in originalJson["data"])
                {
                    var cardJson = new JObject();
                    cardJson["name"] = card["name"];
                    cardJson["type"] = card["type"];
                    cardJson["frameType"] = card["frameType"];
                    cardJson["desc"] = card["desc"];
                    cardJson["atk"] = card["atk"];
                    cardJson["def"] = card["def"];
                    cardJson["level"] = card["level"];
                    cardJson["race"] = card["race"];
                    cardJson["attribute"] = card["attribute"];
                    cardJson["card_images"] = card["card_images"];

                    listCardJson.Add(cardJson);
                }

                var newJson = new JArray(listCardJson).ToString();

                string path = "/";
                File.WriteAllText(path,newJson);
            }
        }
    }
}