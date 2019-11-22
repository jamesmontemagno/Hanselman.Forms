using System;
using Android.App;
using Android.Runtime;
using Hanselman;
using Plugin.CurrentActivity;

namespace HanselmanAndroid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
            
        }


        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
            Shiny.AndroidShinyHost.Init(this, new Startup());
        }
    }
}