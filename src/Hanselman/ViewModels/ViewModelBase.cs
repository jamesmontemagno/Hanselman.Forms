using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using MvvmHelpers;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        IDataService dataService;
        protected IDataService DataService => dataService ?? (dataService = DependencyService.Get<IDataService>());

        protected Page CurrentPage => Application.Current.MainPage;

        protected Task DisplayAlert(string title, string message, string cancel) =>
            CurrentPage.DisplayAlert(title, message, cancel);
    }
}
