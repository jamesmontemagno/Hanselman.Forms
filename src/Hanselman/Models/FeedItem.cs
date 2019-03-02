using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Humanizer;
using Xamarin.Forms;

namespace Hanselman
{
    public class FeedItem : INotifyPropertyChanged
    {
        public string Description { get; set; }
        public string Link { get; set; }

        string publishDate;
        public string PublishDate
        {
            get => publishDate;
            set
            {
                if (DateTime.TryParse(value, out var time))
                    publishDate = time.ToLocalTime().Humanize();
                else
                    publishDate = value;
            }
        }
        public string Author { get; set; }
        public string AuthorEmail { get; set; }
        public int Id { get; set; }
        public string CommentCount { get; set; }
        public string Category { get; set; }

        public string Mp3Url { get; set; }

        string title;
        public string Title
        {
            get => title;
            set => title = value;
        }

        string caption;

        public string Caption
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(caption))
                    return caption;


                //get rid of HTML tags
                caption = Regex.Replace(Description, "<[^>]*>", string.Empty);


                //get rid of multiple blank lines
                caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

                caption = caption.Substring(0, caption.Length < 200 ? caption.Length : 200).Trim() + "...";
                return caption;
            }
        }

        public string Length { get; set; }

        public bool ShowImage { get; set; } = true;

        string image = @"https://secure.gravatar.com/avatar/70148d964bb389d42547834e1062c886?s=60&r=x&d=http%3a%2f%2fd1iqk4d73cu9hh.cloudfront.net%2fcomponents%2fimg%2fuser-icon.png";

        /// <summary>
        /// When we set the image, mark show image as true
        /// </summary>
        public string Image
        {
            get => image;
            set
            {
                image = value;
                ShowImage = true;
            }

        }

        string firstImage;
        public string FirstImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(firstImage))
                    return firstImage;


                var regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);
                var matches = regx.Matches(Description);

                if (matches.Count == 0)
                    firstImage = ScottHead;
                else
                    firstImage = matches[0].Value;

                return firstImage;
            }
        }

        public ImageSource FirstImageSource
        {
            get
            {
                var image = FirstImage;
                return UriImageSource.FromUri(new Uri(image));
            }
        }

        public string ScottHead => "http://www.hanselman.com/images/photo-scott-tall.jpg";

        decimal progress = 0.0M;
        public decimal Progress
        {
            get => progress;
            set { progress = value; OnPropertyChanged("Progress"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
