using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public static class HanselmanApp
	{


		private static Page homeView;
		public static Page RootPage
		{
			get { return homeView ?? (homeView = new HomeView ()); }
		}
	}
}

