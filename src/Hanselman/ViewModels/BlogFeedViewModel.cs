using System.Threading.Tasks;
using System.Linq;
using MvvmHelpers;
using Hanselman.Models;
using System.Windows.Input;
using System;
using MvvmHelpers.Commands;
using System.Diagnostics;

// SvavaBlount cheered 10 on Oct. 18 2019
// SvavaBlount cheered 10 on Oct. 18 2019
// SvavaBlount cheered 10 on Oct. 18 2019
// SvavaBlount cheered 10 on Oct. 18 2019
// SvavaBlount cheered 10 on Oct. 18 2019
// jorian57 cheered 10 on Oct. 18 2019
// jorian57 cheered 20 on Oct. 18 2019
// h0usebesuch cheered 200 on November 8th 2019

namespace Hanselman.ViewModels
{
    public class BlogFeedViewModel : ViewModelBase
    {
        public ObservableRangeCollection<BlogFeedItem> FeedItems { get; }
        public ICommand BlogSelectedCommand { get; }

        public BlogFeedViewModel()
        {
            Title = "Blog";
            Icon = "blog.png";
            FeedItems = new ObservableRangeCollection<BlogFeedItem>();
            BlogSelectedCommand = new AsyncCommand(ExecuteBlogSelectedCommand);
        }

        async Task ExecuteBlogSelectedCommand()
        {
            if (SelectedFeedItem == null)
                return;

            await OpenBrowserAsync(SelectedFeedItem.Link);
            SelectedFeedItem = null;
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        FeedItem? selectedFeedItem;

        public FeedItem? SelectedFeedItem
        {
            get => selectedFeedItem;
            set => SetProperty(ref selectedFeedItem, value);
        }

        ICommand? loadCommand;
        ICommand? refreshCommand;
        public ICommand RefreshCommand =>
            refreshCommand ??= new AsyncCommand(()=>ExecuteLoadCommand(true));

        public ICommand LoadCommand =>
            loadCommand ??= new AsyncCommand<bool>((t)=>ExecuteLoadCommand(t));

        async Task ExecuteLoadCommand(bool forceRefresh)
        {
            if (IsBusy)
                return;
            
            IsBusy = true;
            try
            {
                var items = await DataService.GetBlogItemsAsync(forceRefresh);
                if(items == null)
                {
                    await DisplayAlert("Error", "Unable to load blog.", "OK");
                }
                else
                {
                    FeedItems.ReplaceRange(items);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                await DisplayAlert("Error", "Unable to load blog.", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Gets a specific feed item for an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedItem GetFeedItem(string id) => FeedItems.FirstOrDefault(i => i.Id == id);
    }
}

