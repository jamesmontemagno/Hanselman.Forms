using Windows.Foundation;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hanselman.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Portable.App.IsWindows10 = true;
            Portable.Views.RootPage.IsUWPDesktop = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
            LoadApplication(new Hanselman.Portable.App());

            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(800, 600));
        }
    }
}
