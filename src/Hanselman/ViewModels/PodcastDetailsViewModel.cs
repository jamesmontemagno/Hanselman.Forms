using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Models;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class PodcastDetailsViewModel : ViewModelBase
    {
        public ICommand SubscribeCommand { get; set; }
        public ICommand LoadEpisodesCommand { get; set; }
        public Podcast Podcast { get; set; }
        public ObservableRangeCollection<PodcastEpisode> Episodes { get; set; }

        public PodcastDetailsViewModel()
        {
            SubscribeCommand = new Command(async () => await ExecuteSubscribeCommand());
            LoadEpisodesCommand = new Command(async () => await ExecuteLoadEpisodesCommand());
            Episodes = new ObservableRangeCollection<PodcastEpisode>();
        }
        public PodcastDetailsViewModel(Podcast podcast) : this()
        {
            Podcast = podcast;
        }

        async Task ExecuteSubscribeCommand()
        {
            var services = Podcast
                .PodcastServices
                .Where(s => s.SupportedPlatforms.Contains(DeviceInfo.Platform))
                .Select(s => s.Title);

            var result = await CurrentPage.DisplayActionSheet("Subscribe on:", "Cancel", null,services.ToArray());

            var service = Podcast.PodcastServices.FirstOrDefault(s => s.Title == result);
            if (service == null)
                return;

            await Browser.OpenAsync(service.Url);
        }

        async Task ExecuteLoadEpisodesCommand()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
#if DEBUG
                await Task.Delay(1000);
#endif
                var episodes = await DataService.GetPodcastEpisodesAsync(Podcast.Id, false);
                Episodes.AddRange(episodes);
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
    }
}
