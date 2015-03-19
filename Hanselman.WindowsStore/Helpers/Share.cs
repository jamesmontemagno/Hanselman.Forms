using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Hanselman.Portable.Helpers;
using Hanselman.WindowsStore.Helpers;

[assembly: Xamarin.Forms.Dependency (typeof (Share))]
namespace Hanselman.WindowsStore.Helpers
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