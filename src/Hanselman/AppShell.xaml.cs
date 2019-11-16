using Hanselman.Views;
using Xamarin.Forms;

// AdenEarnshaw cheered 100 August 16, 2019

namespace Hanselman
{
    public partial class AppShell : Shell
    {
        public const string Settings = "sections/tab_about/settings";
        public const string PodcastDetails = "sections/tab_podcasts/podcasts/podcast_details";
        public const string PodcastEpisode = "sections/tab_podcasts/podcasts/podcast_details/episode";
        public const string PodcastPlayback = "sections/tab_podcasts/podcasts/podcast_details/episode/playback";
        public const string VideoSeriesDetails = "sections/tab_videos/videos/series_details";
        public const string VideoEpisodeDetails = "sections/tab_videos/videos/series_details/episode";

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(PodcastDetails, typeof(PodcastDetailsPage));
            Routing.RegisterRoute(PodcastEpisode, typeof(PodcastEpisodePage));
            Routing.RegisterRoute(VideoSeriesDetails, typeof(VideoSeriesPage));
            Routing.RegisterRoute(VideoEpisodeDetails, typeof(VideoDetailsPage));
            Routing.RegisterRoute(Settings, typeof(SettingsPage));
        }
    }
}