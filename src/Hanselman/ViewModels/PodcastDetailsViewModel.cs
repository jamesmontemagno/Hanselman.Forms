using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Models;

namespace Hanselman.ViewModels
{
    public class PodcastDetailsViewModel : ViewModelBase
    {
        public PodcastDetailsViewModel()
        {

        }
        public PodcastDetailsViewModel(Podcast podcast)
        {
            Podcast = podcast;
        }
        public Podcast Podcast { get; set; }
    }
}
