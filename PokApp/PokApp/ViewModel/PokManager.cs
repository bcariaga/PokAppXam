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
            var urlSprites = "http://pokeapi.co/api/v2/pokemon-form/" + searchTerm;

            var uri = urlBase + searchTerm.ToLower();

            var response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();


            var sprite = await client.GetAsync(urlSprites);
            string contentSprite = await sprite.Content.ReadAsStringAsync();

            JObject pokeJson = JObject.Parse(content.ToString());
            JObject jsonSprites = JObject.Parse(contentSprite.ToString());

            Pokemon pokemon = new Pokemon();

            if (response.IsSuccessStatusCode)
            {
                
                pokemon.Name = pokeJson["name"].ToString();
                pokemon.Id = (int)pokeJson["id"];
                pokemon.Descripcion = pokeJson["flavor_text_entries"][3]["flavor_text"].ToString(); //la descripcion en español
                pokemon.Sprite = jsonSprites["sprites"]["front_default"].ToString();

                
            }
            return pokemon;
        }
    }
}
