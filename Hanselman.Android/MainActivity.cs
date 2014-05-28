using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace HanselmanAndroid
{
	[Activity (Label = "Hanselman", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			Forms.Init(this, bundle);

			SetPage (Hanselman.Shared.HanselmanApp.RootPage);
		}
	}
}


