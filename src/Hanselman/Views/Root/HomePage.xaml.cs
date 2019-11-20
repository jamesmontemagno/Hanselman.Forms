using Hanselman.Interfaces;
using Xamarin.Forms;

namespace Hanselman.Views
{
    public partial class HomePage : TabbedPage
    {
        public HomePage ()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            if (CurrentPage is IPageHelpers page)
            {
                page.OnPageVisible();
            }
            else if (CurrentPage is NavigationPage navPage &&
                navPage.CurrentPage is IPageHelpers subPage)
            {
                subPage.OnPageVisible();
            }
        }
    }
}