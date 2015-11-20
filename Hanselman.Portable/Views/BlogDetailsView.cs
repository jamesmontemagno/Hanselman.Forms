using Hanselman.Portable.Helpers;
using Plugin.Share;
using System;
using Xamarin.Forms;

namespace Hanselman.Portable
{
    public class BlogDetailsView : BaseView
    {
        public BlogDetailsView(FeedItem item)
        {
            BindingContext = item;
            var webView = new WebView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            webView.Source = new HtmlWebViewSource
            {
                Html = item.Description
            };
            Content = new StackLayout
            {
                Children =
        {
          webView
        }
            };
            var share = new ToolbarItem
            {
                Icon = "ic_share.png",
                Text = "Share",
                Command = new Command(() => CrossShare.Current
                  .Share("Be sure to read @shanselman's " + item.Title + " " + item.Link))
            };

            ToolbarItems.Add(share);
        }
    }
}

