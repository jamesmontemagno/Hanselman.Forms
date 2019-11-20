using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Helpers;
using Hanselman.ViewModels;
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
            Link = item.Link;
            PublishDate = item.PublishDate;
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

        public string DisplayPublishDate
        {
            get => DateTime.TryParse(PublishDate, out var time) ? time.TwitterHumanize() : PublishDate;
            set { }
        }

        async Task ExecuteReadCommand()
        {
            await ViewModelBase.OpenBrowserAsync(Link);
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
