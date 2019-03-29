using Foundation;
using Hanselman.iOS.Helpers;
using Hanselman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(LaunchTwitter))]
namespace Hanselman.iOS.Helpers
{
    public class LaunchTwitter : ILaunchTwitter
    {
        #region ILaunchTwitter implementation

        public bool OpenUserName(string username)
        {
            try
            {
                if (UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"twitter://user?screen_name={username}")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url" + ex);
            }

            try
            {
                if (UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"tweetbot://{username}/timeline")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url " + ex);
            }
            return false;
        }

        public bool OpenStatus(string statusId)
        {

            try
            {
                if (UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"twitter://status?id={statusId}")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url " + ex);
            }

            try
            {
                if (UIApplication.SharedApplication.OpenUrl(NSUrl.FromString($"tweetbot:///status/{statusId}")))
                    return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch url " + ex);
            }
            return false;
        }

        #endregion


    }
}