using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Xamarin.Forms.Platform.Android;
using HanselmanAndroid.Renderers;
using Android.Media;
using System.Timers;
[assembly: ExportCell(typeof(AudioPlayer), typeof(AudioPlayerRenderer))]
namespace HanselmanAndroid.Renderers
{
    public class AudioPlayerRenderer : FrameRenderer
    {
        MediaPlayer player;

        Timer timer;
        private AudioPlayer Player
        {
            get { return (AudioPlayer)this.Element; }
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;

            player = new MediaPlayer();
            timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;

            player.Prepared += (sender, args) =>
              {
                  try
                  {
                      player.SeekTo(0);
                      player.Start();
                      timer.Start();
                  }
                  catch
                  {
                  }
              };

            if (string.IsNullOrWhiteSpace(Player.Url))
                return;

            InitPlayer();

        }

        private void InitPlayer()
        {
            if (player != null)
                player.Stop();

            player.SetDataSource(Forms.Context, Android.Net.Uri.Parse(Player.Url));
            player.PrepareAsync();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == AudioPlayer.UrlProperty.PropertyName)
            {
                InitPlayer();
            }
            else if (e.PropertyName == AudioPlayer.PlaybackStateProperty.PropertyName)
            {
                if (player == null)
                    return;

                switch (Player.PlaybackState)
                {
                    case 0:
                        player.Start();
                        timer.Start();
                        break;
                    case 1:
                        player.Pause();
                        timer.Stop();
                        break;
                    case 2:
                        player.Stop();
                        timer.Stop();
                        Player.Progress = 0.0M;
                        break;
                }
            }
            else if (e.PropertyName == AudioPlayer.SeekToProperty.PropertyName)
            {
                if (player == null)
                    return;

                player.SeekTo((int)(player.Duration * Player.SeekTo));
            }
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (player == null)
                return;
            var current = player.CurrentPosition;
            var duration = player.Duration;

            Player.Progress = (decimal)current / (decimal)duration;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            player.Stop();
            player.Dispose();
            timer.Stop();
        }
    }
}