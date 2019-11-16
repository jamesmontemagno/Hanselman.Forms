﻿using System.Linq;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;

namespace Hanselman.Views
{
    public partial class MediaDirectoryPage : ContentPage
    {
        MediaDirectoryViewModel VM { get; }
        public MediaDirectoryPage()
        {
            InitializeComponent();
            BindingContext = VM = new MediaDirectoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.LoadPodcastsCommand.Execute(null);
            VM.LoadSeriesCommand.Execute(null);
        }

        async void CollectionViewPodcasts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Podcast podcast)
            {
                await Navigation.PushAsync(new PodcastDetailsPage(podcast));
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        async void CollectionViewVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is VideoSeries series)
            {
                await Navigation.PushAsync(new VideoSeriesPage(series));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}