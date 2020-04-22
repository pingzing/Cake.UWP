using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Cake.UWP
{
    sealed partial class App : Application
    {
        public App()
        {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(640, 375);

            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            MainPage rootElement = Window.Current.Content as MainPage;

            if (rootElement == null)
            {
                rootElement = new MainPage();
                Window.Current.Content = rootElement;
            }

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(640, 375));

            if (e.PrelaunchActivated == false)
            {
                Window.Current.Activate();
            }
        }
    }
}
