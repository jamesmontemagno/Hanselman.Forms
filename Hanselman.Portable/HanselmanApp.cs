using System;
using Xamarin.Forms;

namespace $safeprojectname$.Shared
{
	public static class $safeprojectname$App
	{


		private static Page homeView;
		public static Page RootPage
		{
			get { return homeView ?? (homeView = new HomeView ()); }
		}
	}
}

