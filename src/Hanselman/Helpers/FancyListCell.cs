using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
    class FancyListCell : ViewCell
    {
        public FancyListCell()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start
            };
            image.SetBinding(Image.SourceProperty, new Binding("Icon"));
            image.WidthRequest = image.HeightRequest = 50;


            var nameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 26
            };
            nameLabel.SetBinding(Label.TextProperty, "Title");

            var viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Padding = 15,
                Children = { image, nameLabel }
            };
            View = viewLayout;

        }
    }
}
