using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Phone.Tasks;
using Hanselman.Portable.Helpers;
using Hanselman.WindowsPhone.Helpers;

[assembly: Xamarin.Forms.Dependency (typeof (Share))]
namespace Hanselman.WindowsPhone.Helpers
{
  public class Share : IShare
  {
    public void ShareText(string text)
    {
      var task = new ShareStatusTask
      {
        Status = text
      };

      task.Show();
    }

		public void LaunchBrowser (string url)
		{
			if (string.IsNullOrWhiteSpace (url))
				return;
			try
			{
				WebBrowserTask webBrowserTask = new WebBrowserTask();

				webBrowserTask.Uri = new Uri(url, UriKind.Absolute);

				webBrowserTask.Show();

			}
			catch(Exception ex)
			{
			}
		}

  }
}