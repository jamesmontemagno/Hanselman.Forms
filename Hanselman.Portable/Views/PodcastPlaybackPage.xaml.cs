using Hanselman.Portable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
  public partial class PodcastPlaybackPage : ContentPage
  {
    public PodcastPlaybackPage(FeedItem item)
    {
      InitializeComponent();
      BindingContext = item;
      webView.Source = new HtmlWebViewSource
      {
        Html = item.Description
      };

      var share = new ToolbarItem
      {
        Icon = "ic_share.png",
        Text = "Share",
        Command = new Command(() =>
          {
            DependencyService.Get<IShare>()
          .ShareText("Listening to @shanselman's " + item.Title + " " + item.Link);
          })
      };

      ToolbarItems.Add(share);

      play.Clicked += (sender, args) => player.PlaybackState = 0;
      pause.Clicked += (sender, args) => player.PlaybackState = 1;
      stop.Clicked += (sender, args) => player.PlaybackState = 2;

      if(Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.WinPhone)
      {
        play.Text = "Play";
        pause.Text = "Pause";
        stop.Text = "Stop";
      }

      if(Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
      {
        this.BackgroundColor = Color.White;
        this.title.TextColor = Color.Black;
        this.date.TextColor = Color.Black;
        this.play.TextColor = Color.Black;
        this.pause.TextColor = Color.Black;
        this.stop.TextColor = Color.Black;
      }
 
    }
  }
}
