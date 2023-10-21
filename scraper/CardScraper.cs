using Newtonsoft.Json.Linq;

namespace Scraper
{
    class CardScraper
    {
        public static JObject obtain_json(string url)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Clear();

            var response = client.GetAsync(url).Result;

            var res = response.Content.ReadAsStringAsync().Result;
            JObject originalJson = JObject.Parse(res);
            
            return originalJson;

        }
        public static List<JObject> select_fields(JObject json, bool if_monster)
        {
            var listCardJson = new List<JObject>();

            foreach(var card in json["data"])
            {
                var cardJson = new JObject();
                cardJson["name"] = card["name"];
                cardJson["type"] = card["type"];
                cardJson["frameType"] = card["frameType"];
                cardJson["desc"] = card["desc"];
                if(if_monster)
                {
                    cardJson["atk"] = card["atk"];
                    cardJson["def"] = card["def"];
                    cardJson["level"] = card["level"];
                }
                cardJson["race"] = card["race"];
                if(if_monster)
                {
                    cardJson["attribute"] = card["attribute"];
                }
                cardJson["card_images"] = card["card_images"];

                listCardJson.Add(cardJson);
            }
            return listCardJson;
        }
        public static void create_json(string fileName, string url, bool if_monster ) 
        {
            JObject original_Json = obtain_json(url);

            string newJson = new JArray(select_fields(original_Json, if_monster)).ToString();

            File.WriteAllText(fileName, newJson);
        }  
        static void Main(string[] args)
        {
            // string url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?type=spell%20card";
            // string fileName = "Spells.json"; 

            // create_json(fileName,url,false);

            // url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?type=trap%20card";
            // fileName = "Traps.json";

            // create_json(fileName,url,false);

            // url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?type=normal%20monster,effect%20monster,normal%20tuner%20monster,tuner%20monster,flip%20monster,flip%20effect%20monster,spirit%20monster,flip%20tuner%20effect%20monster,union%20effect%20monster,gemini%20monster,pendulum%20effect%20monster,pendulum%20normal%20monster,pendulum%20tuner%20effect%20monster,ritual%20monster,ritual%20effect%20monster,toon%20monster,fusion%20monster,synchro%20monster,synchro%20tuner%20monster,synchro%20pendulum%20effect%20monster,xyz%20monster,xyz%20pendulum%20effect%20monster,link%20monster,pendulum%20flip%20effect%20monster,pendulum%20effect%20fusion%20monster";   
            // fileName = "Monsters.json";

            // create_json(fileName,url,true);
            
            var arq = ArchetypeScraper.obtain_archetypes();
            File.WriteAllText("Archetype.json", ArchetypeScraper.arquetype_json(arq).ToString());
        }
    }
}