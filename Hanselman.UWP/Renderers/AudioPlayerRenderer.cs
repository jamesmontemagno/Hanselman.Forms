using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Hanselman.UWP.Renderers;

[assembly: ExportCell(typeof(AudioPlayer), typeof(AudioPlayerRenderer))]
namespace Hanselman.UWP.Renderers
{
  public class AudioPlayerRenderer : FrameRenderer
  {
    MediaElement player;
    DispatcherTimer timer;
    private AudioPlayer Player
    {
      get {  return  (AudioPlayer)this.Element;}
    }
    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
    {
      base.OnElementChanged(e);
      if (Element == null)
        return;

      player = new MediaElement();
      player.AutoPlay = true;
      player.MediaOpened += (sender, args) =>
      {

        timer.Start();

      };
      this.Control.Child = player;

      timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
      timer.Tick += timer_Tick;

      if (string.IsNullOrWhiteSpace(Player.Url))
        return;

      InitPlayer();
      
    }


    private void InitPlayer()
    {
      if (player != null)
        player.Pause();

      player.Source = new Uri(Player.Url);
     
    }

    protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            player.Position = new TimeSpan(0);
            break;
        }

      }
      else if(e.PropertyName == AudioPlayer.SeekToProperty.PropertyName)
      {
        if (player == null)
          return;

        var newTime = new TimeSpan(0, 0, (int)(Player.SeekTo * (decimal)player.NaturalDuration.TimeSpan.TotalSeconds));
        player.Position = newTime;
      }
    }


    void timer_Tick(object sender, object e)
    {
      if (player == null || player.NaturalDuration.TimeSpan.TotalSeconds <= 0)
        return;

            try
            {
                Player.Progress = (decimal)(player.Position.TotalSeconds / player.NaturalDuration.TimeSpan.TotalSeconds);
            }catch(Exception ex)
            {
                player.Stop();
            }
    }
  }
}