using Newtonsoft.Json.Linq;
using PokApp.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokApp.ViewModel
{
    public class PokManager 
    {
        public async Task<Pokemon> searchPokemon(string searchTerm) {

            HttpClient client = new HttpClient();

            var urlBase = "http://pokeapi.co/api/v2/pokemon-species/";

            var uri = urlBase + searchTerm.ToLower();

            var response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();

            //Pokemon poke = JsonConvert.DeserializeObject<Pokemon>(content.ToString());
            JObject pokeJson = JObject.Parse(content.ToString());

            Pokemon pokemon = new Pokemon();
            pokemon.Name = pokeJson["name"].ToString();
            pokemon.Id =(int)pokeJson["id"];
            pokemon.Descripcion = pokeJson["flavor_text_entries"][3]["flavor_text"].ToString(); //la descripcion en español

            return pokemon;
        }
    }
}
