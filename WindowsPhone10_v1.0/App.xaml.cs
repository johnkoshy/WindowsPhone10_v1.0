using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WindowsPhone10_v1._0
{
    public sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }
            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPageTemp), e.Arguments); // Updated to MainPageTemp
                }
                Window.Current.Activate();
            }
        }
    }
}