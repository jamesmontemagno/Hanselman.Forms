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
                  Command = new Command(async () =>
                  {
                      
                        await this.Navigation.PushAsync(new WebsiteView("https://m.twitter.com/shanselman", "@shanselman"));
                  })
              });

            facebook.GestureRecognizers.Add(new TapGestureRecognizer()
             {
                 Command = new Command(async () =>
                 {
                     
                        await this.Navigation.PushAsync(new WebsiteView("https://facebook.com/scott.hanselman", "Scott @Facebook"));
                 })
             });

            instagram.GestureRecognizers.Add(new TapGestureRecognizer()
             {
                 Command = new Command(async () =>
                 {
                    
                        await this.Navigation.PushAsync(new WebsiteView("https://instagram.com/shanselman", "Scott @Instagram"));
                 })
             });

            
        }
    }
}
