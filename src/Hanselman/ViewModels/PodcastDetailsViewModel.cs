using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Models;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

// clintonrocksmith cheered 1000 on July 19th 2019

namespace Hanselman.ViewModels
{
    public class PodcastDetailsViewModel : ViewModelBase
    {
        public ICommand SubscribeCommand { get; set; }
        public ICommand LoadEpisodesCommand { get; set; }
        public Podcast? Podcast { get; set; }
        public List<PodcastEpisode> AllEpisodes { get; set; }
        public ObservableRangeCollection<PodcastEpisode> Episodes { get; set; }

        public PodcastDetailsViewModel()
        {
            SubscribeCommand = new Command(async () => await ExecuteSubscribeCommand());
            LoadEpisodesCommand = new Command(async () => await ExecuteLoadEpisodesCommand());
            Episodes = new ObservableRangeCollection<PodcastEpisode>();
            AllEpisodes = new List<PodcastEpisode>();
        }
        public PodcastDetailsViewModel(Podcast podcast) : this()
        {
            Podcast = podcast;
        }

        async Task ExecuteSubscribeCommand()
        {
            if(Podcast == null)
            {
                Debug.WriteLine("Podcast was not set.");
                return;
            }

            var services = Podcast
                .PodcastServices
                .Where(s => s.SupportedPlatforms.Contains(DeviceInfo.Platform))
                .Select(s => s.Title);

            var result = await CurrentPage.DisplayActionSheet("Subscribe on:", "Cancel", null,services.ToArray());

            var service = Podcast.PodcastServices.FirstOrDefault(s => s.Title == result);
            if (service == null)
                return;

            await OpenBrowserAsync(service.Url);
        }

        async Task ExecuteLoadEpisodesCommand()
        {
            if (Podcast == null)
            {
                Debug.WriteLine("Podcast was not set.");
                return;
            }

            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
#if DEBUG
                await Task.Delay(1000);
#endif
                var episodes = await DataService.GetPodcastEpisodesAsync(Podcast.Id, false);

                AllEpisodes.Clear();
                Episodes.Clear();
                CanLoadMore = true;
                AllEpisodes.AddRange(episodes);
                LoadMoreEpisodes();
            }
            catch (System.Exception)
            {
                //stuff
            }
            finally
            {
                IsBusy = false;
            }
        }

        const int chunk = 50;
        public void LoadMoreEpisodes()
        {
            if (!CanLoadMore)
                return;

            var totalLeft = AllEpisodes.Count - Episodes.Count;
            var toGet = totalLeft > chunk ? chunk : totalLeft;
            Episodes.AddRange(AllEpisodes.GetRange(Episodes.Count, toGet));
            CanLoadMore = Episodes.Count != AllEpisodes.Count;
        }
    }
}
