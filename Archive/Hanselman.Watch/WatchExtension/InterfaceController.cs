using System;

using WatchKit;
using Foundation;
using System.Xml.Serialization;
using Hanselman.Portable;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WatchExtension
{
	public partial class InterfaceController : WKInterfaceController
	{
		public InterfaceController (IntPtr handle) : base (handle)
		{
		}

		string Path { get; set;}
		public static List<Tweet> Tweets {get;set;}

		public override void Awake (NSObject context)
		{
			base.Awake (context);
			Tweets = new List<Tweet> ();

			// Configure interface objects here.
			Console.WriteLine ("{0} awake with context", this);

			var FileManager = new NSFileManager ();
			var appGroupContainer = FileManager.GetContainerUrl ("group.com.refractored.hanselman");
      if (appGroupContainer == null)
      {
        Console.WriteLine("You must go into apple developer console and create a new app group");
      }
      else
      {
        Path = System.IO.Path.Combine(appGroupContainer.Path, "tweets.xml");
        Console.WriteLine("agcpath: " + Path);
      }
		}

		public override void WillActivate ()
		{

      if (Tweets.Count > 0 && Tweets[0].Text != "Unable to load tweets")
        return;
      Tweets.Clear();
			// This method is called when the watch view controller is about to be visible to the user.
			Console.WriteLine ("{0} will activate", this);
      if(string.IsNullOrWhiteSpace(Path))
      {
        Console.WriteLine("No Path found, can not load tweets.");
        Tweets.Add(new Tweet
          {
            Text="Unable to load tweets"
          });
        return;
      }

      try
      {
        var json = File.ReadAllText(Path);
        var items = JsonConvert.DeserializeObject<List<Tweet>>(json);
        Tweets.AddRange(items);
			  

			  Console.WriteLine("Tweet count: " + Tweets.Count);

			
      }
      catch
      {
        Console.WriteLine("Unable to load tweets, verify xml has been written.");
        Tweets.Add(new Tweet
          {
            Text="Unable to load tweets"
          });
      }

      TwitterTable.SetNumberOfRows((nint)Tweets.Count, "tweetRow");
      for (var i = 0; i < Tweets.Count; i++)
      {
        var elementRow = (TwitterRow)TwitterTable.GetRowController(i);
        elementRow.Set(Tweets[i]);
      }

		}

		public override NSObject GetContextForSegue (string segueIdentifier, WKInterfaceTable table, nint rowIndex)
		{
			return new NSNumber (rowIndex);
		}
		public override void DidDeactivate ()
		{
			// This method is called when the watch view controller is no longer visible to the user.
			Console.WriteLine ("{0} did deactivate", this);
		}
	}
}

