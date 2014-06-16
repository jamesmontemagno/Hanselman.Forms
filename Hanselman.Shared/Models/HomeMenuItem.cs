using System;

namespace Hanselman.Shared
{
	public enum MenuType
	{
		About,
		Blog,
		Twitter,
        Podcast
	}
	public class HomeMenuItem : BaseModel
	{
		public HomeMenuItem ()
		{
			MenuType = MenuType.About;
		}
		public string Icon {get;set;}
		public MenuType MenuType { get; set; }
	}
}

