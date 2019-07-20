using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    public partial class VideoDirectoryPage : ContentPage, IPageHelpers
    {
        VideoSeriesViewModel VM { get; }
        public VideoDirectoryPage()
        {
            InitializeComponent();
            BindingContext = VM = new VideoSeriesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DeviceInfo.Platform != DevicePlatform.UWP)
                OnPageVisible();

        }

        public void OnPageVisible()
        {
            if (VM.IsBusy || VM.VideoSeries.Count > 0)
                return;

            VM.LoadSeriesCommand.Execute(null);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is View view && view.BindingContext is VideoSeriesViewModel series)
            {
                //await Navigation.PushAsync(new Vi(series));
            }
        }
    }
}