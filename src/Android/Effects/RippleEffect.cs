using System;
using System.Linq;
using Android.Util;
using HanselmanAndroid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(RippleEffect), "RippleEffect")]
namespace HanselmanAndroid.Effects
{
    public class RippleEffect : PlatformEffect
    {
        static int RippleResourceId { get; set; }

        protected override void OnAttached()
        {
            if (Container is Android.Views.View view)
            {

                SetClickable(view);
                SetSelectedItemBackground(view);

                // The TapGesture doesn't work anymore when clickable is set to true
                // In this case redirect the event manually
                view.Click += TapGestureFix;
            }
            else if(Control is Android.Views.View control)
            {
                SetClickable(control);
                SetSelectedItemBackground(control);

                // The TapGesture doesn't work anymore when clickable is set to true
                // In this case redirect the event manually
                control.Click += TapGestureFix;
            }
        }

        protected override void OnDetached()
        {
            try
            {
                if (Container is Android.Views.View view)
                {
                    view.Click -= TapGestureFix;
                }
                else if (Control is Android.Views.View control)
                {
                    control.Click -= TapGestureFix;
                }
            }
            catch (ObjectDisposedException)
            {
                // Sometime the object is already disposed
            }
        }

        void SetClickable(Android.Views.View view)
        {
            view.Clickable = true;
            view.Focusable = true;
        }

        void SetSelectedItemBackground(Android.Views.View view)
        {
            if (RippleResourceId == default)
            {
                using (var outValue = new TypedValue())
                {
                    view.Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, outValue, true);
                    RippleResourceId = outValue.ResourceId;
                }
            }
            view.SetBackgroundResource(RippleResourceId);
        }

        void TapGestureFix(object sender, EventArgs args)
        {
            var view = Element as View;

            var tapGestureRecognizer = view
                ?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
            ?? (view?.Parent as View)
                ?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault();

            tapGestureRecognizer?.Command?.Execute(null);
        }
    }
}