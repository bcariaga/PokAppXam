using PokApp.Model;
using PokApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace PokApp.Views
{
    public class AllPoke : ContentPage
    {
        public AllPoke()
        {
            var layout = new StackLayout();

            var pokManager = new PokManager();

            List < Pokemon > pokeAll = pokManager.GetAllInternal();

            foreach (Pokemon poke in pokeAll)
            {
                layout.Children.Add(new Label {
                    HorizontalOptions = LayoutOptions.Start,
                    InputTransparent = true,
                    Text = poke.Name,
                    TextColor = Color.Black
                });

                layout.Children.Add(new Image
                {
                    HorizontalOptions = LayoutOptions.End,
                    Source = new Uri(poke.Sprite),
                    Aspect = Aspect.AspectFit
                });

                layout.Children.Add(new BoxView {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 2,
                    Color = Color.Black
                });
            }

            Content = new ScrollView { Content = layout };
            
        }
    }
}
