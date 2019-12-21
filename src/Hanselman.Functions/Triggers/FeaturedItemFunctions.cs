using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Hanselman.Functions.Triggers
{
    public static class FeaturedItemFunctions
    {
        [FunctionName("ResizeImage")]
        public static void Run(
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

        private static readonly Dictionary<ImageSize, (int Width, int Height)> imageDimensionsTable = new Dictionary<ImageSize, (int, int)>()
    {
        { ImageSize.ExtraSmall, (320, 200) },
        { ImageSize.Small,      (640, 400) },
        { ImageSize.Medium,     (800, 600) }
    };

        [FunctionName("UploadFeaturedImage")]
        public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "upload/{name}/image")] HttpRequest req,
        [Blob("hanselman-public/featured-images/{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream outBlob,
        string name,
        ILogger log)
        {
            var file = req.Form.Files.First();

            await file.CopyToAsync(outBlob);
            
            return name != null
                ? (ActionResult)new OkObjectResult($"hanselman-public/featured-images/{name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");

           
        }
    }
}
