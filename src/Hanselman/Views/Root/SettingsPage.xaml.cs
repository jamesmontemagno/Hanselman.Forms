using System;
using System.Linq;
using Hanselman.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
#if !DEBUG
            if (Navigation.ModalStack.Any())
                await Navigation.PopModalAsync();
#endif
        }
    }
}