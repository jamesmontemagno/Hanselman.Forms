using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hanselman.Functions.Auth;
using Hanselman.Functions.Helpers;
using Hanselman.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Hanselman.Functions.Triggers
{
    public static class FeaturedItemFunctions
    {
#if !DEBUG
        [FunctionName("ResizeImage")]
        public static void ResizeImageAsync(
        [BlobTrigger("hanselman-public/featured-images/{name}")] Stream image,
        [Blob("hanselman-public/featured-images-sm/{name}", FileAccess.Write)] Stream imageSmall,
        [Blob("hanselman-public/featured-images-md/{name}", FileAccess.Write)] Stream imageMedium)
        {
            IImageFormat format;

            using (var input = Image.Load(image, out format))
            {
                ResizeImage(input, imageSmall, ImageSize.Small, format);
            }

            image.Position = 0;
            using (var input = Image.Load(image, out format))
            {
                ResizeImage(input, imageMedium, ImageSize.Medium, format);
            }
        }
#endif

        public static void ResizeImage(Image input, Stream output, ImageSize size, IImageFormat format)
        {
            var max = imageDimensionsTable[size];
            var ratioX = (double)max.Width / input.Width;
            var ratioY = (double)max.Height / input.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(input.Width * ratio);
            var newHeight = (int)(input.Height * ratio);
            input.Mutate(x => x.Resize(newWidth, newHeight));
            input.Save(output, format);
        }

        public enum ImageSize { ExtraSmall, Small, Medium }

        static readonly Dictionary<ImageSize, (int Width, int Height)> imageDimensionsTable = new Dictionary<ImageSize, (int, int)>()
    {
        { ImageSize.ExtraSmall, (320, 200) },
        { ImageSize.Small,      (640, 400) },
        { ImageSize.Medium,     (800, 600) }
    };

        static async Task<bool> ValidateUser(HttpRequest req)
        {
            var authInfo = await AuthInfoExtensions.GetAuthInfoAsync(req);

            if (authInfo.ProviderName.ToLowerInvariant() != "twitter")
                return false;

            var user = authInfo.UserId.ToLowerInvariant();
            return user == "jamesmontemagno" || user == "shanselman";
        }

        [FunctionName(nameof(GetFeaturedItems))]
        public static HttpResponseMessage GetFeaturedItems(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/featured.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            return BlobHelpers.BlobToHttpResponseMessage(inBlob, log, "featured");
        }

        [FunctionName("RemoveFeaturedItem")]
        public static async Task<IActionResult> RemoveFeaturedItem(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
           [Blob("hanselman/featured.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
           [Blob("hanselman/featured.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlob,
           ILogger log)
        {
            if (!await ValidateUser(req))
            {
                return new UnauthorizedResult();
            }

            string id = req.Query["id"];

  
            if (string.IsNullOrWhiteSpace("id"))
                return new BadRequestObjectResult("Invalid featured item post.");

            var currentFeatured = BlobHelpers.BlobToItems<FeaturedItem>(inBlob, log, "featured");

            var index = currentFeatured.FindIndex(i => i.Id == id);

            currentFeatured.RemoveAt(index);

            var json = JsonConvert.SerializeObject(currentFeatured, Formatting.None);

            log.LogInformation("Writting featured items to blob.");
            using (var writer = new StreamWriter(outBlob))
            {
                writer.Write(json);
            }


            log.LogInformation("Featured Items function finished.");

            return (ActionResult)new OkObjectResult("OK");
        }


        [FunctionName("UpdateAllFeaturedItems")]
        public static async Task<IActionResult> UpdateAllFeaturedItems(
           [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
           [Blob("hanselman/featured.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlob,
           ILogger log)
        {
            if (!await ValidateUser(req))
            {
                return new UnauthorizedResult();
            }

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var items = JsonConvert.DeserializeObject<List<FeaturedItem>>(content);
            if (items == null || items.Count == 0)
                return new BadRequestObjectResult("Invalid featured item post.");
                      
         

            var json = JsonConvert.SerializeObject(items, Formatting.None);

            log.LogInformation("Writting featured items to blob.");
            using (var writer = new StreamWriter(outBlob))
            {
                writer.Write(json);
            }


            log.LogInformation("Featured Items function finished.");

            return (ActionResult)new OkObjectResult("OK");
        }

        [FunctionName("UpdateFeaturedItem")]
        public static async Task<IActionResult> UpdateFeaturedItem(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [Blob("hanselman/featured.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            [Blob("hanselman/featured.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlob,
            ILogger log)
        {
            if (!await ValidateUser(req))
            {
                return new UnauthorizedResult();
            }

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var item = JsonConvert.DeserializeObject<FeaturedItem>(content);
            if (item == null)
                return new BadRequestObjectResult("Invalid featured item post.");

            if (string.IsNullOrWhiteSpace(item.Image) || string.IsNullOrWhiteSpace(item.Link) ||
                string.IsNullOrWhiteSpace(item.Title))
                return new BadRequestObjectResult("Invalid featured item post.");

            var currentFeatured = BlobHelpers.BlobToItems<FeaturedItem>(inBlob, log, "featured");

            var index = currentFeatured.FindIndex(i => i.Id == item.Id);

            currentFeatured.RemoveAt(index);

            currentFeatured.Insert(index, item);

            var json = JsonConvert.SerializeObject(currentFeatured, Formatting.None);

            log.LogInformation("Writting featured items to blob.");
            using (var writer = new StreamWriter(outBlob))
            {
                writer.Write(json);
            }


            log.LogInformation("Featured Items function finished.");

            return (ActionResult)new OkObjectResult("OK");
        }

        [FunctionName("AddFeaturedItem")]
        public static async Task<IActionResult> AddFeaturedItem(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [Blob("hanselman/featured.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            [Blob("hanselman/featured.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlob,
            ILogger log)
        {
            if (!await ValidateUser(req))
            {
                return new UnauthorizedResult();
            }

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var item = JsonConvert.DeserializeObject<FeaturedItem>(content);
            if (item == null)
                return new BadRequestObjectResult("Invalid featured item post.");

            if(string.IsNullOrWhiteSpace(item.Image) || string.IsNullOrWhiteSpace(item.Link) ||
                string.IsNullOrWhiteSpace(item.Title))
                return new BadRequestObjectResult("Invalid featured item post.");

            var currentFeatured = BlobHelpers.BlobToItems<FeaturedItem>(inBlob, log, "featured");

            currentFeatured.Add(item);

            var json = JsonConvert.SerializeObject(currentFeatured, Formatting.None);

            log.LogInformation("Writting featured items to blob.");
            using (var writer = new StreamWriter(outBlob))
            {
                writer.Write(json);
            }


            log.LogInformation("Featured Items function finished.");

            return (ActionResult)new OkObjectResult("OK");
        }

        [FunctionName("UploadFeaturedImage")]
        public static async Task<IActionResult> UploadFeaturedImage(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "upload/{name}/image")] HttpRequest req,
        [Blob("hanselman-public/featured-images/{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream outBlob,
        string name,
        ILogger log)
        {
            
            if(!await ValidateUser(req))
            {
                return new UnauthorizedResult();
            }

            var file = req.Form.Files.First();

            await file.CopyToAsync(outBlob);

#if DEBUG
            var finalUrl = $"http://127.0.0.1:10000/devstoreaccount1/hanselman-public/featured-images/{name}";
#else
            var finalUrl = $"https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/featured-images/{name}";
#endif

            return name != null
                ? (ActionResult)new OkObjectResult(finalUrl)
                : new BadRequestObjectResult("invalid image uploaded, please try again");


        }
    }
}
