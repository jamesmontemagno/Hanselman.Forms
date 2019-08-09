using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hanselman.Models;

// AdenEarnshaw cheered 200 August 2, 2019

namespace Hanselman.ViewModels
{
    public class VideoDetailsViewModel : ViewModelBase
    {
        public VideoDetailsViewModel()
        {

        }

        public VideoDetailsViewModel(VideoFeedItem video)
        {
            Video = video;

            VideoId = video.Id;

            var videos = video.VideoUrls.Where(v => v.Type?.Contains("mp4") ?? false).ToList();
            if (videos.Count == 1)
                VideoUrl = videos[0].Url;
            else if(videos.Count > 1)
            {
                var mid = videos.FirstOrDefault(v => v.Url.Contains("_mid"));
                if (mid != null)
                    VideoUrl = mid.Url;
                else
                    VideoUrl = videos.OrderBy(v => v.FileSize).ElementAt(videos.Count / 2).Url;
            }
        }

        public string VideoId { get; set; }
        public VideoFeedItem Video { get; set; }

        public string VideoUrl { get; }


    }
}
