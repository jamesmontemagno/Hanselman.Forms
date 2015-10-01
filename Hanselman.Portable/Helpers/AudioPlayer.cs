using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
    public class AudioPlayer : Frame
    {
        /// <summary>
        /// Url to playback
        /// </summary>
        public static readonly BindableProperty UrlProperty =
          BindableProperty.Create<AudioPlayer, string>(
            p => p.Url, string.Empty);

        /// <summary>
        /// Gets or sets the url
        /// </summary>
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }


        public static readonly BindableProperty PlaybackStateProperty =
          BindableProperty.Create<AudioPlayer, int>(
            p => p.PlaybackState, 0);

        public int PlaybackState
        {
            get { return (int)GetValue(PlaybackStateProperty); }
            set { SetValue(PlaybackStateProperty, value); }
        }


        public static readonly BindableProperty ProgressProperty =
          BindableProperty.Create<AudioPlayer, decimal>(
            p => p.Progress, (decimal)0.0);

        public decimal Progress
        {
            get { return (decimal)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly BindableProperty SeekToProperty =
          BindableProperty.Create<AudioPlayer, decimal>(
            p => p.SeekTo, (decimal)0.0);

        public decimal SeekTo
        {
            get { return (decimal)GetValue(SeekToProperty); }
            set { SetValue(SeekToProperty, value); }
        }
    }
}
