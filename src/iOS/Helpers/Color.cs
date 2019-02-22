using System;

#if __IOS__
using UIKit;
using CoreGraphics;
#endif

namespace Hanselman.Portable.Helpers
{
    public struct Color
    {
        public static readonly Color Purple = 0xB455B6;
        public static readonly Color Blue = 0x3498DB;
        public static readonly Color DarkBlue = 0x3498DB;
        public static readonly Color Green = 0x77D065;
        public static readonly Color Gray = 0x738182;
        public static readonly Color LightGray = 0xB4BCBC;

        public double R, G, B;

        public static Color FromHex(int hex)
        {
            Func<int, int> at = offset => (hex >> offset) & 0xFF;
            return new Color
            {
                R = at(16) / 255.0,
                G = at(8) / 255.0,
                B = at(0) / 255.0
            };
        }

        public static implicit operator Color(int hex)
        {
            return FromHex(hex);
        }

#if __IOS__
		public UIColor ToUIColor ()
		{
			return UIColor.FromRGB ((float)R, (float)G, (float)B);
		}

		public static implicit operator UIColor (Color color)
		{
			return color.ToUIColor ();
		}

		public static implicit operator CGColor (Color color)
		{
			return color.ToUIColor ().CGColor;
		}
#endif

        public Xamarin.Forms.Color ToFormsColor()
        {
            return Xamarin.Forms.Color.FromRgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
        }

#if __ANDROID__
		public global::Android.Graphics.Color ToAndroidColor ()
		{
			return global::Android.Graphics.Color.Rgb ((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}

		public static implicit operator global::Android.Graphics.Color (Color color)
		{
			return color.ToAndroidColor ();
		}
#endif
    }
}