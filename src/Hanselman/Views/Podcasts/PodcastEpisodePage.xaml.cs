using System;
using Hanselman.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views.Podcasts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PodcastEpisodePage : ContentPage
    {
        public PodcastEpisodePage(PodcastEpisode episode)
        {
            InitializeComponent();
            BindingContext = episode;
        }
        public PodcastEpisodePage()
        {
            InitializeComponent();
        }

        private async void ButtonClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}