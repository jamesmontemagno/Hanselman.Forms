using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Hanselman.Functions.Helpers
{
    static class BlobHelpers
    {
        internal static HttpResponseMessage BlobToHttpResponseMessage(Stream inBlob, ILogger log, string name)
        {
            try
            {
                log.LogInformation($"Reading feed to {name}.");
                var json = string.Empty;
                using (var reader = new StreamReader(inBlob))
                {
                    json = reader.ReadToEnd();
                }

                log.LogInformation($"Finished reading {name} feed from stream.");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Unable to get {name} feed");
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
