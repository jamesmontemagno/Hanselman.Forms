using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Hanselman.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class PodcastEpisodeViewModel : ViewModelBase
    {
        public PodcastEpisode? Episode { get; set; }
        public Command PlayPodcastCommand { get; }

        public PodcastEpisodeViewModel()
        {
            PlayPodcastCommand = new Command(async () => await PlayPodcastAsync());
        }

        public PodcastEpisodeViewModel(PodcastEpisode episode) : this()
        {
            Episode = episode;
        }

        async Task PlayPodcastAsync()
        {
            if (Episode == null)
            {
                Debug.WriteLine("Episode was not set.");
                return;
            }

            try
            {
                await Launcher.OpenAsync(Episode.Mp3Url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
