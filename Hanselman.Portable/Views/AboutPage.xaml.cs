using Hanselman.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
  public partial class AboutPage : ContentPage
  {
    public AboutPage()
    {
      InitializeComponent();

      twitter.GestureRecognizers.Add(new TapGestureRecognizer()
        { 
          Command = new Command(() =>
          {
            this.Navigation.PushAsync(new WebsiteView("http://m.twitter.com/shanselman", "@shanselman"));
          })
        });

     facebook.GestureRecognizers.Add(new TapGestureRecognizer()
      {
        Command= new Command(() =>
        {
          this.Navigation.PushAsync(new WebsiteView("http://facebook.com/scott.hanselman", "Scott @Facebook"));
        })
      });

     instagram.GestureRecognizers.Add(new TapGestureRecognizer()
      {
        Command = new Command(() =>
        {
          this.Navigation.PushAsync(new WebsiteView("http://instagram.com/shanselman", "Scott @Instagram"));
        })
      });

      google.GestureRecognizers.Add(new TapGestureRecognizer()
      {
        Command = new Command(() =>
        {
          this.Navigation.PushAsync(new WebsiteView("http://plus.google.com/108573066018819777334?rel=me", "Hanselman+"));
        })
      });
    }
  }
}
