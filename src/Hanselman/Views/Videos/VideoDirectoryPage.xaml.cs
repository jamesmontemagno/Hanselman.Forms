using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using Hanselman.Models;
using Hanselman.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    public partial class VideoDirectoryPage : ContentPage, IPageHelpers
    {
        VideoDirectoryViewModel VM { get; }
        public VideoDirectoryPage()
        {
            InitializeComponent();
            BindingContext = VM = new VideoDirectoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnPageVisible();
        }

        public void OnPageVisible()
        {
            if (VM.IsBusy || VM.VideoSeries.Count > 0)
                return;

            VM.LoadSeriesCommand.Execute(null);
        }


        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is VideoSeries series && series != null)
            {
                await Shell.Current.GoToAsync($"{AppShell.VideoSeriesDetails}?{series.UriRoute}");
                ((ListView)sender).SelectedItem = null;
            }
        }

        void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}