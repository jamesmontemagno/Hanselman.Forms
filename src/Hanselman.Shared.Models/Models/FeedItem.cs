namespace Hanselman.Models
{
    public class FeedItem
    {
        public FeedItem()
        {
        }

        public string Link { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
        public string CommentCount { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string FirstImage { get;set; }
    }
}
