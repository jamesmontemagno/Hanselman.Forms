using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Helpers;
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
                    Icon = IconConstants.TwitterCircle,
                    Url = "https://www.twitter.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.FacebookBox,
                    Url = "https://www.facebook.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.Instagram,
                    Url = "https://www.instagram.com/shanselman"
                }
            };
        }
    }
}
