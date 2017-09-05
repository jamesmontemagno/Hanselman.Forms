﻿using $safeprojectname$.Portable.Helpers;
using Plugin.Share;
using System;
using Xamarin.Forms;

namespace $safeprojectname$.Portable
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
                  .Share(new Plugin.Share.Abstractions.ShareMessage
                  {
                      Text = "Be sure to read @shanselman's " + item.Title + " " + item.Link,
                      Title = "Share",
                      Url = item.Link
                  }))
            };

            ToolbarItems.Add(share);
        }
    }
}

