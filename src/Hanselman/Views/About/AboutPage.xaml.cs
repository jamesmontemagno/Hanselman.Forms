
using Hanselman.ViewModels;
using Xamarin.Forms;

//mjfreelanding cheered 100 bits on December 10th 2019

namespace Hanselman.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel? vm;
        AboutViewModel? VM => vm ??= BindingContext as AboutViewModel;

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(VM?.FeaturedItems?.Count == 0)
            {
                VM.LoadCommand.Execute(null);
            }
        }
    }
}
