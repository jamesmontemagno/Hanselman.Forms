using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Models;
using Hanselman.ViewModels;
using MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoDetailsPage : ContentPage
    {
        public VideoDetailsPage(VideoFeedItem item) : this()
        {
            BindingContext = new VideoDetailsViewModel(item);
        }
        public VideoDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //CrossMediaManager.Current.Stop();
        }
    }
}