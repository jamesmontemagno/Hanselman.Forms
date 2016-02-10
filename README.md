Hanselman.Forms: Hanselman Everywhere
===============

Built with C# 6 features, you must be running VS 2015 or Xamarin Studio to compile. 

The most awesome Hanselman app built in about 4 hours to showcase a sample of [Xamarin.Forms](http://www.xamarin.com/forms). Be sure to read [Scott's](http://www.twitter.com/shanselman) blog: http://www.hanselman.com/blog/XamarinFormsWriteOnceRunEverywhereANDBeNative.aspx

![All](Screenshots/HanselmanAll.png)


In this sample Hanselman app we show awesome information about the awesome man who is Scott Hanselman including reading in his blog & parsing with XDocument. Additionally, I use Linq2Twitter PCL to grab all of Scott's tweets, all using a nice MVVM Style and nearly 100% code reuse.

To learn more about Xamarin.Forms visit: http://www.xamarin.com/forms

To learn about developing native iOS, Android, and Windows apps in C# visit: http://www.xamarin.com

Screenshots - iOS, Android, & Windows Phone
===
![Main](Screenshots/HanselmanAbout.png)
![Blog](Screenshots/HanselmanFlyout.png)
![Slideout](Screenshots/HanselmanContent.png)

Windows Store Preview
===
Xamarin.Forms is now available in PREVIEW for Windows Store and Windows Phone 8.1 RT platforms. You can see a full example now for Hanselman.Forms and read the full documentation at: http://developer.xamarin.com/guides/cross-platform/xamarin-forms/windows/getting-started/

![HanselStore](Screenshots/HanselmanStore.png)

Android Wear
===
To setup Android wear simply install the main Hanselman app on your Android Phone and then the **WearApp** on your Android Wear device. HanselWear uses Google Play Services to syncronize Tweets from the main application. The code for this sync can be found in WearService.cs.

To find out more how to get started with Android Wear with Xamarin please visit: http://developer.xamarin.com/guides/android/wear/

![HanselWear](Screenshots/HanselWear2.png)
![HanselWear](Screenshots/HanselWear.png)

Apple Watch
===
HanselWatch uses simple app group data to sync tweets. You must setup an app group id in your developer portal. To find out more about creating Apple Watch apps with Xamarin please visit: http://developer.xamarin.com/guides/ios/watch/

![HanselWatch](Screenshots/HanselWatch.png)
