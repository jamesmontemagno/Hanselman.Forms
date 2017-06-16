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
        Dictionary<MenuType, NavigationPage> Pages { get; set;} 
        public RootPage()
        {
            if(IsUWPDesktop)
                this.MasterBehavior = MasterBehavior.Popover;

            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel
                {
                    Title = "Hanselman",
                    Icon = "slideout.png"
                };
            //setup home page
            NavigateAsync(MenuType.About);

            InvalidateMeasure();
        }



        public async Task NavigateAsync(MenuType id)
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
                    case MenuType.About:
                        Pages.Add(id, new HanselmanNavigationPage(new AboutPage()));
                        break;
                    case MenuType.Blog:
                        Pages.Add(id, new HanselmanNavigationPage(new BlogPage()));
                        break;
                    case MenuType.DeveloperLife:
                        Pages.Add(id, new HanselmanNavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Hanselminutes:
                        Pages.Add(id, new HanselmanNavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Ratchet:
                        Pages.Add(id, new HanselmanNavigationPage(new PodcastPage(id)));
                        break;
                    case MenuType.Twitter:
                        Pages.Add(id, new HanselmanNavigationPage(new TwitterPage()));
                        break;
                    case MenuType.Videos:
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

