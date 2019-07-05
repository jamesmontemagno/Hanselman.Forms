using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Models;
using Xamarin.Forms;

namespace Hanselman.Views.Twitter
{
    public class TweetDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TweetTemplate { get; set; }
        public DataTemplate TweetWithMediaTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Tweet)item).HasMedia ? TweetWithMediaTemplate : TweetTemplate;
        }
    }
}
