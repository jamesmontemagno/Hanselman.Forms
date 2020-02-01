Hanselman.Forms: Hanselman Everywhere
===============
The most awesome Hanselman app built originall in about 4 hours to showcase a sample of Xamarin.Forms. Be sure to read [Scott's blog](http://www.hanselman.com/blog/XamarinFormsWriteOnceRunEverywhereANDBeNative.aspx) on the app. 

Since release we have worked open source on [Twitch](https://twitch.tv/jamesmontemagno) to use the latest features of Xamarin.Forms and awesome plugins from the community including:

* Audio & Video Playback
* Background Syncronization
* Azure Functions Optimized Backend
* Azure Logic App Updating
* Pretty UI with Xamarin.Forms Shell, CollectionView, Material Visual, and more

In this sample Hanselman app we show awesome information about the awesome man who is Scott Hanselman including reading in his blog, tweets, podcasts, and videos. All of this using a nice MVVM Style and nearly 100% code reuse.

To learn more about Xamarin.Forms visit: http://www.xamarin.com/forms

To learn about developing native iOS, Android, and Windows apps in C# with Xamarin visit: http://www.xamarin.com


## Build Status

| Build Server | Type            | Platform | Status                                                                                                                                                                                 |
|--------------|-----------------|----------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Azure DevOps | App Build       | macOS    | [![Build status](https://dev.azure.com/jamesmontemagno/Hanselman.Forms/_apis/build/status/Hanselman.Forms%20App)](https://dev.azure.com/jamesmontemagno/Hanselman.Forms/_build/latest?definitionId=30) |                                           |
| Azure DevOps | Functions Build | Windows  | [![Build status](https://dev.azure.com/jamesmontemagno/Hanselman.Forms/_apis/build/status/Hanselman.Forms%20-%20Functions%20CI)](https://dev.azure.com/jamesmontemagno/Hanselman.Forms/_build/latest?definitionId=35) |

## Download

Development build available:

| Source | Platform | QR Code |                                                          
|--------------|-----------------|----------------|
| App Center | [Android](https://install.appcenter.ms/orgs/hanselman.forms/apps/hanselman.forms-android/distribution_groups/public%20testers) | ![](art/download_android.png) |


### Made Possible By
* [GlideX](https://github.com/jonathanpeppers/glidex)
* [Humanizer](https://github.com/Humanizr/Humanizer)
* [Image Circle Plugin for Xamarin.Forms](https://github.com/jamesmontemagno/ImageCirclePlugin)
* [Media Manager](https://github.com/martijn00/XamarinMediaManager)
* [🐵Monkey Cache](https://github.com/jamesmontemagno/monkey-cache)
* [MvvmHelpers](https://github.com/jamesmontemagno/mvvm-helpers)
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
* [PancakeView for Xamarin.Forms](https://github.com/sthewissen/Xamarin.Forms.PancakeView)
* [Pull to Refresh Plugin for Xamarin.Forms](https://github.com/jamesmontemagno/Xamarin.Forms-PullToRefreshLayout)
* [Shiny](https://github.com/shinyorg/shiny)
* [Xamarin.Essentials](https://docs.microsoft.com/xamarin/essentials)
* [Xamarin.Forms](https://xamarin.com/forms)
* [Xamarin.Forms Visual](https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/visual/)

### Awesome Tools
* [IconFont2Code](https://andreinitescu.github.io/IconFont2Code/): Turns your fonts into code!
* [Material Design Icons](https://materialdesignicons.com/): Awesome icons for your app


### Run Twitter Auth Locally
* run `ngrok http 5000`
* Update auth redirect on Azure Function
* Update Cores on Azure Functions
* Update Twitter App with Url
* Update code "constants.cs" with url
* RUN THE FUNCTION :)
* AuthInfo extensions change url to `https://yoursite.azurewebsites.net/`