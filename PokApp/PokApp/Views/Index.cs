using PokApp.Model;
using PokApp.ViewModel;
using System;
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

            Button getAll = new Button {
                Text = "Ver los guardados.",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                WidthRequest = 100,
                HeightRequest = 100,
            };

            getAll.Clicked += GetAll_Clicked;


            SearchBar busqueda = new SearchBar();
            busqueda.SearchButtonPressed += async (sender, e) =>
            {
                Pokemon pokemon = await Busqueda_Pokemon(busqueda.Text);

                try
                {
                    dibujarPokemon(layout, pokemon);
                    
                }
                catch (Exception)
                {
                    await DisplayAlert("Oh oh!", "no se encotro un Pokemon con ese nombre", "volver");
                    return;
                }

            };
            layout.Children.Add(busqueda);
            

            Content = new ScrollView { Content = layout };
            //layout.Children.Add(getAll);
        }

        private void GetAll_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AllPoke();
            //await Navigation.PushModalAsync(new AllPoke());

        }

        private async Task<Pokemon> Busqueda_Pokemon(string pokeNombre)
        {
            PokManager pokManager = new PokManager();

            Pokemon pokeResult = await pokManager.searchPokemon(pokeNombre);


            return pokeResult;
        }

        private void dibujarPokemon(Layout<View> layout, Pokemon pokemon) {

            if (pokemon != null)
            {
                layout.Children.Clear();
            }
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
                VerticalOptions = LayoutOptions.End
            };

            Label descripcion = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = pokemon.Descripcion,
                FontAttributes =  FontAttributes.Italic,
                VerticalOptions = LayoutOptions.Start
            };

            UriImageSource spriteLocal = new UriImageSource
            {
                Uri = new Uri(pokemon.Sprite),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(10, 0, 0, 0)

            };
            Image sprite = new Image
            {
                Source = spriteLocal,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Start,

            };

            layout.Children.Add(pokeId);
            layout.Children.Add(nombre);
            layout.Children.Add(sprite);
            layout.Children.Add(descripcion);
        }

       
    }
}
