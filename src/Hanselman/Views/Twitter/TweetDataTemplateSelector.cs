using System;
using Hanselman.Models;
using Xamarin.Forms;

namespace Hanselman.Views.Twitter
{
    public class TweetDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? TweetTemplate { get; set; }
        public DataTemplate? TweetWithMediaTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            if (TweetTemplate == null)
                throw new NullReferenceException($"{nameof(TweetTemplate)} was null");

            if (TweetWithMediaTemplate == null)
                throw new NullReferenceException($"{nameof(TweetWithMediaTemplate)} was null");

            return ((Tweet)item).HasMedia ? TweetWithMediaTemplate : TweetTemplate;
        }
    }
}
