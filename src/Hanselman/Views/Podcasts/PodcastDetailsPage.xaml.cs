using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Controls;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// chrisntr cheered 200 on March 22nd 2019


namespace Hanselman.Views.Podcasts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PodcastDetailsPage : ContentPage
    {
        PodcastDetailsViewModel VM => (PodcastDetailsViewModel)BindingContext;
        public PodcastDetailsPage()
        {
            InitializeComponent();
        }

        public PodcastDetailsPage(Podcast podcast) : this()
        {
            BindingContext = new PodcastDetailsViewModel(podcast);
        }

        private async void ButtonClose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.LoadEpisodesCommand.Execute(null);
        }

        void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}