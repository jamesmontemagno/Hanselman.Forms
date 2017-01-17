using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using $safeprojectname$.Portable;
using System.IO;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency (typeof ($safeprojectname$.iOS.iOSTweetStore))]


namespace $safeprojectname$.iOS
{
	public class iOSTweetStore : ITweetStore
	{
		public void Save (System.Collections.Generic.List<$safeprojectname$.Portable.Tweet> tweets)
		{

			var FileManager = new Foundation.NSFileManager ();
      var appGroupContainer = FileManager.GetContainerUrl("group.com.refractored.hanselman");
      if(appGroupContainer == null)
      {
        Console.WriteLine("You must go into apple developer console and create a new app group");
     
        return;
      }
			var path = System.IO.Path.Combine(appGroupContainer.Path, "tweets.xml");
			Console.WriteLine ("agcpath: " + path);


      var json = JsonConvert.SerializeObject(tweets);

      File.WriteAllText(path, json);


			/*var serializer = new XmlSerializer(typeof(List<Tweet>));
			using (var stream = File.Open(path, FileMode.CreateNew, FileAccess.ReadWrite))
			{
				serializer.Serialize(stream, tweets);
			}*/
		}
		//System.Collections.Generic.List<$safeprojectname$.Shared.Tweet> Load ();
	}
}

