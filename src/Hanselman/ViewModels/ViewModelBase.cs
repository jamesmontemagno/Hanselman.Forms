using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Interfaces;
using MvvmHelpers;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        IDataService dataService;
        public IDataService DataService => dataService ?? (dataService = DependencyService.Get<IDataService>());
    }
}
