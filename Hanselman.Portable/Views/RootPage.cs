using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using Hanselman.Portable.Controls;

namespace Hanselman.Portable.Views
{
    public class RootPage : MasterDetailPage
    {
        public static bool IsUWPDesktop { get; set; }
        Dictionary<int, NavigationPage> Pages { get; set;} 
        public RootPage()
        {
            if(IsUWPDesktop)
                this.MasterBehavior = MasterBehavior.Popover;

            Pages = new Dictionary<int, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel
                {
                    Title = "Hanselman",
                    Icon = "slideout.png"
                };
            //setup home page
            Pages.Add((int)MenuType.About, new HanselmanNavigationPage(new AboutPage()));
            Detail = Pages[(int)MenuType.About];

            InvalidateMeasure();
        }



        public async Task NavigateAsync(int id)
        {

            if (Detail != null)
            {
                if (IsUWPDesktop || Device.Idiom != TargetIdiom.Tablet)
                    IsPresented = false;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(300);
            }

            Page newPage;
            if (!Pages.ContainsKey(id))
            {

                switch (id)
                {
                    case (int)MenuType.About:
                        Pages.Add(id, new HanselmanNavigationPage(new AboutPage()));
                        break;
                    case (int)MenuType.Blog:
                        Pages.Add(id, new HanselmanNavigationPage(new BlogPage()));
                        break;
                    case (int)MenuType.DeveloperLife:
                        Pages.Add(id, new HanselmanNavigationPage(new PodcastPage((MenuType)id)));
                        break;
                    case (int)MenuType.Hanselminutes:
                        Pages.Add(id, new HanselmanNavigationPage(new PodcastPage((MenuType)id)));
                        break;
                    case (int)MenuType.Ratchet:
                        Pages.Add(id, new HanselmanNavigationPage(new PodcastPage((MenuType)id)));
                        break;
                    case (int)MenuType.Twitter:
                        Pages.Add(id, new HanselmanNavigationPage(new TwitterPage()));
                        break;
                    case (int)MenuType.Videos:
                        Pages.Add(id, new HanselmanNavigationPage(new Channel9VideosPage()));
                        break;
                }
            }

            newPage = Pages[id];
            if(newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.RuntimePlatform == Device.WinPhone)
            {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }
    }
}

