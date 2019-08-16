using System;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views.Podcasts
{
    public partial class PodcastEpisodePage : ContentPage
    {
        public PodcastEpisodePage(PodcastEpisode episode, string title)
        {
            InitializeComponent();
            BindingContext = new PodcastEpisodeViewModel(episode)
            {
                Title = title
            };
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