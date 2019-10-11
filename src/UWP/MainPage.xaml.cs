using Hanselman.Styles;
using MediaManager;
using Windows.Foundation;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Xamarin.Forms;
using Size = Windows.Foundation.Size;
using Application = Windows.UI.Xaml.Application;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hanselman.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        private UISettings uiSettings = new UISettings();
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new Hanselman.App());

            CrossMediaManager.Current.Init();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(480, 600));
            uiSettings.ColorValuesChanged += UISettings_ColorValuesChanged;
        }

        private void UISettings_ColorValuesChanged(UISettings sender, object args)
        {
            ThemeHelper.ChangeTheme(Application.Current.RequestedTheme == Windows.UI.Xaml.ApplicationTheme.Dark ? Models.Theme.Dark : Models.Theme.Light);
        }
    }
}
