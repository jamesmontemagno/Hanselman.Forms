using System;

namespace Hanselman.Models
{
    public enum MenuType
    {
        About,
        Blog,
        Twitter,
        Hanselminutes,
        Ratchet,
        DeveloperLife,
        Videos
    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.About;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}

