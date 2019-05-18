using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.Models
{
    public class BlogFeedItem : FeedItem
    {
        public ICommand ShareCommand { get; }
        public ICommand ReadCommand { get; }
        public BlogFeedItem(FeedItem item) : this()
        {
            Author = item.Author;
            Caption = item.Caption;
            Category = item.Category;
            CommentCount = item.CommentCount;
            FirstImage = item.FirstImage;
            Id = item.Id;
            Image = item.Image;
            Link = item.Link;
            PublishDate = item.PublishDate;
            ShowImage = item.ShowImage;
            Title = item.Title;
        }
        public BlogFeedItem()
        {
            ShareCommand = new Command(async () => await ExecuteShareCommand());
            ReadCommand = new Command(async () => await ExecuteReadCommand());
        }
        async Task ExecuteShareCommand()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = Link,
                Title = "Share",
                Text = Title,
                Subject = Caption
            });
        }

        string displayPublishDate;
        public string DisplayPublishDate
        {
            get => DateTime.TryParse(PublishDate, out var time) ? time.TwitterHumanize() : PublishDate;
            set => displayPublishDate = value;
        }

        async Task ExecuteReadCommand()
        {
            await Browser.OpenAsync(Link, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredControlColor = Color.White,
                PreferredToolbarColor = (Color)Application.Current.Resources["PrimaryColor"]
            });
        }

        public ImageSource FirstImageSource
        {
            get
            {
                var image = FirstImage;
                return ImageSource.FromUri(new Uri(image));
            }
        }
    }
}
