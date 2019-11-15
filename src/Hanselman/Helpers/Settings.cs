using System;
using Hanselman.Models;
using Xamarin.Essentials;

namespace Hanselman.Helpers
{
    public static class Settings
    {
        const long defaultPosition = 0;
        public static long GetPlaybackPosition(string id) =>
            Preferences.Get(id, defaultPosition);

        public static void SavePlaybackPosition(string id, long position) =>
            Preferences.Set(id, position);

        public static string PlaybackId
        {
            get => Preferences.Get(nameof(PlaybackId), string.Empty);
            set => Preferences.Set(nameof(PlaybackId), value);
        }
        public static string PlaybackUrl
        {
            get => Preferences.Get(nameof(PlaybackUrl), string.Empty);
            set => Preferences.Set(nameof(PlaybackUrl), value);
        }

        public static Theme ThemeOption
        {
            get => (Theme)Preferences.Get(nameof(ThemeOption), HasDefaultThemeOption ? (int)Theme.Default : (int)Theme.Light);
            set => Preferences.Set(nameof(ThemeOption), (int)value);
        }

        public static bool HasDefaultThemeOption
        {
            get
            {
                var minDefaultVersion = new Version(13, 0);
                if (DeviceInfo.Platform == DevicePlatform.UWP)
                    minDefaultVersion = new Version(10, 0, 17763, 1);
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                    minDefaultVersion = new Version(10, 0);

                return DeviceInfo.Version >= minDefaultVersion;
            }
        }
    }
}
