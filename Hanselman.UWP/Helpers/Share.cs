using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Hanselman.UWP.Helpers;

[assembly: Xamarin.Forms.Dependency (typeof (Share))]
namespace Hanselman.UWP.Helpers
{
  public class Share : IShare
  {
    public void ShareText(string text)
    {
      
    }

		public void LaunchBrowser (string url)
		{
			
		}

  }
}