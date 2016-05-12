using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views.Animations;
using Hanselman.Portable;
using System.Linq;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.Support.Wearable.Activity;
using System.Collections.Generic;

namespace WearApp
{
  [Activity(Label = "HanselWear",
             Theme = "@android:style/Theme.DeviceDefault.Light",
             MainLauncher = true, Icon = "@drawable/ic_launcher",
             NoHistory = true,
             StateNotNeeded = true)]
  public class MainActivity : Activity,
    IDataApiDataListener, GoogleApiClient.IConnectionCallbacks, 
    GoogleApiClient.IOnConnectionFailedListener, IResultCallback
  {
    const string TweetsPath = "/hanselman/Tweets";

    GridViewPager viewPager;
    ProgressBar progress;
    GoogleApiClient client;
    INode phoneNode;
    Handler handler;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      handler = new Handler();
      client = new GoogleApiClient.Builder(this, this, this)
        .AddApi(WearableClass.API)
        .Build();
      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      viewPager = FindViewById<GridViewPager>(Resource.Id.pager);
      progress = FindViewById<ProgressBar>(Resource.Id.progressBar);

    }


    protected override void OnStart()
    {
      base.OnStart();
      client.Connect();
    }

    public void OnDataChanged(DataEventBuffer dataEvents)
    {
      var dataEvent = Enumerable.Range(0, dataEvents.Count)
        .Select(i => dataEvents.Get(i).JavaCast<IDataEvent>())
        .FirstOrDefault(de => de.Type == DataEvent.TypeChanged && de.DataItem.Uri.Path == TweetsPath + "/Answer");
      if (dataEvent == null)
        return;
      var dataMapItem = DataMapItem.FromDataItem(dataEvent.DataItem);
      var map = dataMapItem.DataMap;

      var tweets = new List<Tweet>();
      var data = map.GetDataMapArrayList("Tweets");
      foreach (var d in data)
      {
        tweets.Add(new Tweet
        {
          ScreenName = d.GetString("ScreenName", "<no name>"),
          Text = d.GetString("Text", "<no name>"),
          CreatedAt = new DateTime(d.GetLong("CreatedAt", DateTime.Now.Ticks))
        });
      }

      if (tweets.Any())
      {
        handler.Post(() =>
        {
          var adapter = new TweetAdapter(FragmentManager,
                                            tweets);
          viewPager.Adapter = adapter;
          viewPager.OffscreenPageCount = 5;
          progress.Visibility = ViewStates.Gone;
          viewPager.Visibility = ViewStates.Visible;
        });
      }
    }

    public void OnConnected(Bundle p0)
    {
      WearableClass.DataApi.AddListener(client, this);
      GetTweets();
    }

    void DisplayError()
    {
      Finish();
      var intent = new Intent(this, typeof(ConfirmationActivity));
      intent.PutExtra(ConfirmationActivity.ExtraAnimationType, ConfirmationActivity.FailureAnimation);
      intent.PutExtra(ConfirmationActivity.ExtraMessage, "Can't find phone");
      StartActivity(intent);
    }

    protected override void OnStop()
    {
      base.OnStop();
      client.Disconnect();
    }

    public void OnConnectionSuspended(int reason)
    {
      Android.Util.Log.Error("GMS", "Connection suspended " + reason);
      WearableClass.DataApi.RemoveListener(client, this);
    }

    public void OnConnectionFailed(Android.Gms.Common.ConnectionResult result)
    {
      Android.Util.Log.Error("GMS", "Connection failed " + result.ErrorCode);
    }

    void GetTweets()
    {
      WearableClass.NodeApi.GetConnectedNodes(client)
        .SetResultCallback(this);
    }

    public void OnResult(Java.Lang.Object result)
    {
      var apiResult = result.JavaCast<INodeApiGetConnectedNodesResult>();
      var nodes = apiResult.Nodes;
      phoneNode = nodes.FirstOrDefault();
      if (phoneNode == null)
      {
        DisplayError();
        return;
      }

      WearableClass.MessageApi.SendMessage(client, phoneNode.Id,
                                            TweetsPath,
                                            new byte[0]);
    }
  }
}


