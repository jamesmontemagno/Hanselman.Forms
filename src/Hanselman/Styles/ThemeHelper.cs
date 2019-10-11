using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Helpers;
using Hanselman.Interfaces;
using Hanselman.Models;
using Xamarin.Forms;

namespace Hanselman.Styles
{
    public static class ThemeHelper
    {
        public static Theme CurrentTheme = Settings.ThemeOption;

        public static void ChangeTheme(Theme theme, bool forceTheme = false)
        {
            // don't change to the same theme
            if (theme == CurrentTheme && !forceTheme)
                return;

            //// clear all the resources
            //Application.Current.Resources.MergedDictionaries.Clear();
            //Application.Current.Resources.Clear();
            var applicationResourceDictionary = Application.Current.Resources;
            ResourceDictionary newTheme = null;

            if (theme == Theme.Default)
            {
                var environment = DependencyService.Get<IEnvironment>();
                theme = environment?.GetOSTheme() ?? Theme.Light;
            }

            switch (theme)
            {
                case Theme.Light:
                    newTheme = new LightTheme();
                    break;
                case Theme.Dark:
                    newTheme = new DarkTheme();
                    break;
            }

            foreach (var merged in newTheme.MergedDictionaries)
            {
                applicationResourceDictionary.MergedDictionaries.Add(merged);
            }

            ManuallyCopyThemes(newTheme, applicationResourceDictionary);

            CurrentTheme = theme;
        }

        static void ManuallyCopyThemes(ResourceDictionary fromResource, ResourceDictionary toResource)
        {
            foreach (var item in fromResource.Keys)
            {
                toResource[item] = fromResource[item];
            }
        }
    }
}
