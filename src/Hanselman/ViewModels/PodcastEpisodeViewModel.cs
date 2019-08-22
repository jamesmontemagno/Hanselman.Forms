using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Models;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class PodcastEpisodeViewModel : ViewModelBase
    {
        public PodcastEpisode Episode { get; set; }
        public Command PlayPodcastCommand { get; }

        public PodcastEpisodeViewModel()
        {
            PlayPodcastCommand = new Command(async () => await PlayPodcastAsync());
        }

        public PodcastEpisodeViewModel(PodcastEpisode episode) :
            this()
        {
            Episode = episode;
        }

        async Task PlayPodcastAsync()
        {
            try
            {
                await Launcher.OpenAsync(Episode.Mp3Url);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
