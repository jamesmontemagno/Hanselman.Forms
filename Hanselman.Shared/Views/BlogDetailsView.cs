using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public class BlogDetailsView : BaseView
	{
		public BlogDetailsView (FeedItem item)
		{
			BindingContext = item;
		    Content = new WebView { Source = item.Link };
		}
	}
}

