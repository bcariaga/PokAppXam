using Newtonsoft.Json.Linq;
using PokApp.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Realms;
using System.Linq;
using System.Collections.Generic;

namespace PokApp.ViewModel
{
    public class PokManager 
    {
        private async Task<Pokemon> searchPokemonExternal(string searchTerm)
        {

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

        //private Pokemon searchPokemonInternal(string searchTerm)
        //{
        //    var real = Realm.GetInstance();

        //    Pokemon pokeResult =  real.All<Pokemon>().Where(p => p.Name == searchTerm ).First();
            
        //    return pokeResult;
        //}
        public async Task<Pokemon> searchPokemon(string searchTerm) {

            var realm = Realm.GetInstance();
            Pokemon pokeResult = new Pokemon();

            try
            {
                pokeResult = realm.All<Pokemon>().Where(p => p.Name == searchTerm).First();
            }
            catch (System.Exception)
            {

                pokeResult = await searchPokemonExternal(searchTerm);

                using (var transaction = realm.BeginWrite())
                {
                    realm.Add(pokeResult);
                    transaction.Commit();
                }
            }
            
            //if (pokeResult == null)
            //{
            //    pokeResult = await searchPokemonExternal(searchTerm);

            //    using (var transaction = realm.BeginWrite())
            //    {
            //        realm.Add(pokeResult);
            //        transaction.Commit();
            //    }
            //}

            return pokeResult;
        }

        public List<Pokemon> GetAllInternal() {

            var realm = Realm.GetInstance();

            List < Pokemon > pokeList = (List<Pokemon>) realm.All<Pokemon>();

            return pokeList;

        }

    }
}
