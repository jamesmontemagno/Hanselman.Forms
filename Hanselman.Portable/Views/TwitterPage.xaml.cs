using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
  public partial class TwitterPage : ContentPage
  {
    private TwitterViewModel ViewModel
    {
      get { return BindingContext as TwitterViewModel; }
    }

    public TwitterPage()
    {
      InitializeComponent();

      BindingContext = new TwitterViewModel();


      listView.ItemTapped += (sender, args) =>
      {
        if (listView.SelectedItem == null)
          return;
        var tweet = listView.SelectedItem as Tweet;
        this.Navigation.PushAsync(new WebsiteView("http://m.twitter.com/shanselman/status/" + tweet.StatusID, tweet.Date));
        listView.SelectedItem = null;
      };
    }


    protected override void OnAppearing()
    {
      base.OnAppearing();
      if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Tweets.Count > 0)
        return;

      ViewModel.LoadTweetsCommand.Execute(null);
    }
  }
}
