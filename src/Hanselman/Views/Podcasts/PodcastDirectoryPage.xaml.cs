using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Controls;
using Hanselman.Interfaces;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
	public partial class PodcastDirectoryPage : ContentPage, IPageHelpers
    {
        PodcastDirectoryViewModel VM { get; }
		public PodcastDirectoryPage ()
		{
			InitializeComponent ();
            VM = (PodcastDirectoryViewModel)BindingContext;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnPageVisible();
        }

        public void OnPageVisible()
        {
            if (VM.IsBusy || VM.Podcasts.Count > 0)
                return;

            VM.LoadPodcastsCommand.Execute(null);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(sender is View view && view.BindingContext is Podcast podcast)
            {
                await Navigation.PushAsync(new PodcastDetailsPage(podcast));
            }
        }
    }
}