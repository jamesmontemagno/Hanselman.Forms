using System;
using System.Text.RegularExpressions;

namespace Hanselman.Shared
{
  public class FeedItem
  {



    public string Description { get; set; }
    public string Link { get; set; }
		private string publishDate;
    public string PublishDate 
		{
			get { return publishDate; }
			set{
				DateTime time; 
				if (DateTime.TryParse (value, out time))
					publishDate = time.ToLocalTime().ToString ("f");
				else
					publishDate = value;
			}
		}
    public string Author { get; set; }
    public string AuthorEmail { get; set; }
    public int Id { get; set; }
		public string CommentCount { get; set; }
		public string Category { get; set; }


    private string title;
    public string Title
    {
      get
      {
        return title;
      }
      set
      {
        title = value;
       
      }
    }

    private string caption;

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

        return caption;
      }
    }

    private bool showImage = true;

    public bool ShowImage
    {
      get { return showImage; }
      set { showImage = value; }
    }

    private string image = @"https://secure.gravatar.com/avatar/70148d964bb389d42547834e1062c886?s=60&r=x&d=http%3a%2f%2fd1iqk4d73cu9hh.cloudfront.net%2fcomponents%2fimg%2fuser-icon.png";

    /// <summary>
    /// When we set the image, mark show image as true
    /// </summary>
    public string Image
    {
      get { return image; }
      set
      {
        image = value;
        showImage = true;
      }

    }

  }
}
