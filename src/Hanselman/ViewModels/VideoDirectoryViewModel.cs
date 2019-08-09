using System;
using Hanselman.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class VideoDirectoryViewModel : ViewModelBase
    {
        public ObservableRangeCollection<VideoSeries> VideoSeries { get; } = new ObservableRangeCollection<VideoSeries>();
        public ICommand LoadSeriesCommand { get; }
        public VideoDirectoryViewModel()
        {
            LoadSeriesCommand = new Command(async () => await ExecuteLoadVideoSeries());
        }

        async Task ExecuteLoadVideoSeries()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var pods = await DataService.GetVideoSeriesAsync(false);
                if (VideoSeries.Count > 0)
                    VideoSeries.ReplaceRange(pods);
                else
                    VideoSeries.AddRange(pods);
            }
            catch (Exception ex)
            {
#if DEBUG
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
#endif
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
