using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Hanselman.Interfaces;
using Hanselman.Models;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Application = Windows.UI.Xaml.Application;

[assembly: Dependency(typeof(Hanselman.UWP.Helpers.Environment))]
namespace Hanselman.UWP.Helpers
{
    public class Environment : IEnvironment
    {
        public Theme GetOSTheme()
        {   
            switch(Application.Current.RequestedTheme)
            {
                case ApplicationTheme.Dark:
                    return Theme.Dark;
                case ApplicationTheme.Light:
                    return Theme.Light;
            }

            return Theme.Light;
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
        }
    }
}