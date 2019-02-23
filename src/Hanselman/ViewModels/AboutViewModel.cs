using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Models;
using MvvmHelpers;

namespace Hanselman.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public List<SocialItem> SocialItems { get; }
        public AboutViewModel()
        {
            SocialItems = new List<SocialItem>
            {
                new SocialItem
                {
                    Icon = "twitter.png",
                    Url = "https://www.twitter.com/shanselman"
                },
                new SocialItem
                {
                    Icon = "facebook.png",
                    Url = "https://www.facebook.com/shanselman"
                },
                new SocialItem
                {
                    Icon = "instagram.png",
                    Url = "https://www.instagram.com/shanselman"
                }
            };
        }
    }
}
