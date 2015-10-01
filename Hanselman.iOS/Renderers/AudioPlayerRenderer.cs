using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Hanselman.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using AVFoundation;
using System.Timers;
using CoreMedia;
[assembly: ExportCell(typeof(AudioPlayer), typeof(AudioPlayerRenderer))]
namespace Hanselman.iOS.Renderers
{
  public class AudioPlayerRenderer : FrameRenderer
  {
    AVPlayer player;
    Timer timer;
    private AudioPlayer Player
    {
      get {  return  (AudioPlayer)this.Element;}
    }
    protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
    {
      base.OnElementChanged(e);
      if (Element == null)
        return;

      timer = new Timer(1000);
      timer.Elapsed += timer_Elapsed;

      if (string.IsNullOrWhiteSpace(Player.Url))
        return;

      InitPlayer();
      
    }

    private void InitPlayer()
    {
      if (player != null)
        player.Pause();

      player = AVPlayer.FromUrl(new NSUrl(Player.Url));
      player.Play();
      timer.Start();
    }

    protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);
      if(e.PropertyName == AudioPlayer.UrlProperty.PropertyName)
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
            player.Play();
            timer.Start();
            break;
          case 1:
            player.Pause();
            timer.Stop();
            break;
          case 2:
            player.Pause();
            timer.Stop();
            Player.Progress = 0.0M;
            await player.SeekAsync(new CoreMedia.CMTime(0, 1));
            break;
        }

      }
      else if(e.PropertyName == AudioPlayer.SeekToProperty.PropertyName)
      {
        if (player == null || player.CurrentItem == null)
          return;

        var newTime = CMTime.FromSeconds((double)(Player.SeekTo * (decimal)player.CurrentItem.Duration.Seconds), 1);
        await player.SeekAsync(newTime);
        if (Player.PlaybackState == 0)
          player.Play();
      }
    }


    void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (player == null)
        return;

      Player.Progress = (decimal)(player.CurrentItem.CurrentTime.Seconds / player.CurrentItem.Duration.Seconds);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      player.Pause();
      player.Dispose();
      timer.Stop();
    }
  }
}