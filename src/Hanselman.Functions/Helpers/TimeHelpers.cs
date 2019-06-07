using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hanselman.Functions.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hanselman.Functions.Helpers
{
    static class TimeHelpers
    {
        internal static bool CheckIfNewEntry(Stream outStream, Stream inStream, ILogger log, string lastEntryTimeString)
        {
            UpdateTimeStamp timeStamp = null;
            var saveTimeStamp = false;
            var sendPushNotification = false;
            if (inStream == null)
            {
                log.LogInformation("No stamp file exists :(");
                timeStamp = new UpdateTimeStamp { LastUpdate = DateTimeOffset.UtcNow };
                saveTimeStamp = true;
            }
            else if (DateTimeOffset.TryParse(lastEntryTimeString, out var lastEntryDateTime))
            {
                log.LogInformation($"Latest entry: {lastEntryDateTime}");
                UpdateTimeStamp previousTimeStamp = null;
                using (var reader = new StreamReader(inStream))
                {
                    var timeJson = reader.ReadToEnd();
                    previousTimeStamp = JsonConvert.DeserializeObject<UpdateTimeStamp>(timeJson);
                }

                log.LogInformation($"Previous entry  time: {previousTimeStamp.LastUpdate}");
                if (lastEntryDateTime > previousTimeStamp.LastUpdate)
                {
                    log.LogInformation("New item! Should send push notification!");
                    timeStamp = new UpdateTimeStamp { LastUpdate = lastEntryDateTime };
                    saveTimeStamp = true;
                    sendPushNotification = true;
                }
            }

            if (saveTimeStamp)
            {
                using (var writer = new StreamWriter(outStream))
                {
                    writer.Write(JsonConvert.SerializeObject(timeStamp));
                }
            }

            return sendPushNotification;
        }
    }
}
