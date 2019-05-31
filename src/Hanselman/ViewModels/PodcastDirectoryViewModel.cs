using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class PodcastDirectoryViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Podcast> Podcasts { get; } = new ObservableRangeCollection<Podcast>();
        public ICommand LoadPodcastsCommand { get; }
        public PodcastDirectoryViewModel()
        {
            LoadPodcastsCommand = new Command(async () => await ExecuteLoadPodcasts());
        }

        async Task ExecuteLoadPodcasts()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var pods = await DataService.GetPodcastsAsync(false);
                if (Podcasts.Count > 0)
                    Podcasts.ReplaceRange(pods);
                else
                    Podcasts.AddRange(pods);
                //OnPropertyChanged(nameof(Podcasts));
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
