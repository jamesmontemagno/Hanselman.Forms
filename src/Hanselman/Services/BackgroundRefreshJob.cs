using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shiny.Jobs;

namespace Hanselman.Services
{
    public class BackgroundRefreshJob : Shiny.Jobs.IJob
    {
        DataService dataService;
        public BackgroundRefreshJob()
        {
            dataService = new DataService();
        }


        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            try
            {
                var getTweets = dataService.GetTweetsAsync(true);
                var getBlogs = dataService.GetBlogItemsAsync(true);

                await Task.WhenAll(getTweets, getBlogs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to refresh items: {ex}");
                return false;
            }

            return true;
        }

    }

}
