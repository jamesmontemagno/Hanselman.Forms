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

namespace Hanselman.Views.Podcasts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PodcastDetailsPage : ContentPage
    {
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
    }
}