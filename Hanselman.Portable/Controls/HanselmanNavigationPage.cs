using System;
using Xamarin.Forms;

namespace $safeprojectname$.Portable.Controls
{
    public class $safeprojectname$NavigationPage :NavigationPage
    {
        public $safeprojectname$NavigationPage(Page root) : base(root)
        {
            Init();
        }

        public $safeprojectname$NavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Color.FromHex("#03A9F4");
            BarTextColor = Color.White;
        }
    }
}

