using System;
using Xamarin.Forms;

namespace Hanselman
{
    public class WebsiteView : ContentPage
    {
        public WebsiteView(string site, string title)
        {
            Title = title;
            var webView = new WebView();
            webView.Source = new UrlWebViewSource
            {
                Url = site
            };
            Content = webView;
        }
    }
}

