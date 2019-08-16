using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
