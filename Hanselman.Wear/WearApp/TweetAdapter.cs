using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Wearable.Views;
using Hanselman.Portable;

namespace WearApp
{
  public class TweetAdapter : FragmentGridPagerAdapter
  {
    List<Tweet> tweets;
    public TweetAdapter(FragmentManager p0, List<Tweet> tweets)
      : base(p0)
    {
      this.tweets = tweets;
    }

    public override int GetColumnCount(int row)
    {
      return 1;
    }

    public override int RowCount
    {
      get
      {
        return tweets.Count;
      }
    }

    public override Android.App.Fragment GetFragment(int row, int column)
    {
      return CardFragment.Create(tweets[row].Date, tweets[row].Text);
    }
  }
}