using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views.Twitter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TweetCell : ViewCell
    {
        public TweetCell()
        {
            InitializeComponent();
        }
    }
}