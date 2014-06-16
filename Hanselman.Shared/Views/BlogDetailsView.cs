﻿using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public class BlogDetailsView : BaseView
	{
		public BlogDetailsView (BlogFeedItem item)
		{
			BindingContext = item;
			var webView = new WebView ();
			webView.Source = new HtmlWebViewSource {
				Html = item.Description
			};
			Content = webView;
		}
	}
}

