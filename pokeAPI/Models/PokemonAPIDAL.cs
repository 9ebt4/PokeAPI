using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System.Net;

namespace pokeAPI.Models
{
    public class PokemonAPIDAL
    {
        public static pokemon SearchPokedex()
        {

            string url = $"https://pokeapi.co/api/v2/pokedex/6/";
            //request API
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Converting to json
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            //adjust
            //Convert to c#
            //install Newtonsoft.json
            pokemon result = JsonConvert.DeserializeObject<pokemon>(json);
            return result;
        }
    }
}
