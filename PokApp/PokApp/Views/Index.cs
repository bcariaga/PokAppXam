using PokApp.Model;
using PokApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokApp.Views
{
    public class Index : ContentPage
    {
        public Index()
        {
            var layout = new StackLayout();

            SearchBar busqueda = new SearchBar();
            layout.Children.Add(busqueda);
            busqueda.SearchButtonPressed += async (sender, e) =>
            {
                Pokemon pokemon = await Busqueda_Pokemon(busqueda.Text);
                

                if (pokemon != null)
                {
                    dibujarPokemon(layout, pokemon);
                }

            };

            Content = layout;
        }

        private async Task<Pokemon> Busqueda_Pokemon(string pokeNombre)
        {
            PokManager pokManager = new PokManager();

            Pokemon pokeResult = await pokManager.searchPokemon(pokeNombre);

            if ( pokeResult == null )
            {
                await DisplayAlert("Oh oh!", "no se encotro un Pokemon con ese nombre", "volver");
                return null;
            }

            return pokeResult;
        }

        private void dibujarPokemon(Layout<View> layout, Pokemon pokemon) {

            Label pokeId = new Label
            {
                Text = pokemon.Descripcion,
                IsVisible = false
            };
           

            Label nombre = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = pokemon.Name,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            Label descripcion = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = pokemon.Descripcion,
                FontAttributes =  FontAttributes.Italic,
            };
            layout.Children.Add(pokeId);
            layout.Children.Add(nombre);
            layout.Children.Add(descripcion);
        }

       
    }
}
