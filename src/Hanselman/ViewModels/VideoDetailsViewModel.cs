using System;
using System.Linq;
using Hanselman.Models;
using Xamarin.Forms;

// AdenEarnshaw cheered 200 August 2, 2019

namespace Hanselman.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(SeriesId), nameof(SeriesId))]
    public class VideoDetailsViewModel : ViewModelBase
    {
        string id = string.Empty;
        public string Id
        {
            get => id;
            set => id = Uri.UnescapeDataString(value);
        }

        string seriesId = string.Empty;
        public string SeriesId
        {
            get => seriesId;
            set => seriesId = Uri.UnescapeDataString(value);
        }


        string videoUrl = string.Empty;
        public string VideoUrl
        {
            get => videoUrl;
            set => SetProperty(ref videoUrl, value);
        }

        public Command LoadVideoCommand { get; }

        public VideoDetailsViewModel()
        {
            LoadVideoCommand = new Command(LoadVideo);
        }

        void LoadVideo()
        {
            var video = DataService.GetVideoEpisode(SeriesId, Id);

            if (video == null)
                return;

            Title = video.Title;

            var videos = video.VideoUrls.Where(v => v.Type?.Contains("mp4") ?? false).ToList();
            if (videos.Count == 1)
                VideoUrl = videos[0].Url;
            else if (videos.Count > 1)
            {
                var mid = videos.FirstOrDefault(v => v.Url.Contains("_mid"));
                if (mid != null)
                    VideoUrl = mid.Url;
                else
                    VideoUrl = videos.OrderBy(v => v.FileSize).ElementAt(videos.Count / 2).Url;
            }
        }
    }
}
