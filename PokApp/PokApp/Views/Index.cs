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
            Title = "PokApp";
            Padding = new Thickness(0, 20, 0, 0);
            var layout = new StackLayout();

            SearchBar busqueda = new SearchBar();
            layout.Children.Add(busqueda);
            busqueda.SearchButtonPressed += async (sender, e) =>
            {
                Pokemon pokemon = await Busqueda_Pokemon(busqueda.Text);

                try
                {
                    dibujarPokemon(layout, pokemon);
                }
                catch (Exception)
                {
                    await DisplayAlert("Oh oh!", "no se encotro un Pokemon con ese nombre", "volver"); throw;
                }

            };

            Content = new ScrollView { Content = layout };
        }

        private async Task<Pokemon> Busqueda_Pokemon(string pokeNombre)
        {
            PokManager pokManager = new PokManager();

            Pokemon pokeResult = await pokManager.searchPokemon(pokeNombre);


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

            Image sprite = new Image
            {
                Source = new Uri(pokemon.Sprite),
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,

            };

            layout.Children.Add(pokeId);
            layout.Children.Add(nombre);
            layout.Children.Add(sprite);
            layout.Children.Add(descripcion);
        }

       
    }
}
