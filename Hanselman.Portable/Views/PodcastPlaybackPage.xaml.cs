using Plugin.Share;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
  public partial class PodcastPlaybackPage : ContentPage
  {
    private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;
    public PodcastPlaybackPage(FeedItem item)
    {
      InitializeComponent();
      BindingContext = item;
      CrossMediaManager.Current.PlayingChanged += (sender, args) => ProgressBar.Progress = args.Progress;
      CrossMediaManager.Current.Play(new MediaFile(item.Mp3Url, MediaFileType.Audio));
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
           CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
           {
               Text = "Listening to @shanselman's " + item.Title + " " + item.Link,
               Title = "Share",
               Url = item.Link
           });
          })
      };

      ToolbarItems.Add(share);

      play.Clicked += (sender, args) => PlaybackController.Play();
      pause.Clicked += (sender, args) => PlaybackController.Pause();
      stop.Clicked += (sender, args) => PlaybackController.Stop();

      if(Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.WinPhone || App.IsWindows10)
      {
        play.Text = "Play";
        pause.Text = "Pause";
        stop.Text = "Stop";
      }

      if((Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) && !App.IsWindows10)
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
