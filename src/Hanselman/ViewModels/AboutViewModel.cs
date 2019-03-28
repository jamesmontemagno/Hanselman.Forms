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
                    Icon = IconConstants.Twitter,
                    Url = "https://www.twitter.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.Facebook,
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
