using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CloudGenshin;

public sealed partial class MainPage : Page
{
    private string desktopUserAgent = "";
    private string mobileUserAgent = "";

    public MainPage()
    {
        InitializeComponent();


        Window.Current.SetTitleBar(TitleBarRegion);

        var dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        WebView.EnsureCoreWebView2Async().AsTask().ContinueWith(task =>
        {
            dispatcherQueue.TryEnqueue(() =>
            {
                desktopUserAgent = (WebView.CoreWebView2.Settings.UserAgent);
                mobileUserAgent = desktopUserAgent.Replace("Windows NT 10.0;", "Linux; Android 12.0;").Replace("Safari/", "Mobile Safari/");
            });
        });

        ApplicationView.GetForCurrentView().VisibleBoundsChanged += (s, e) =>
        {
            if (s.IsFullScreenMode)
            {
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
                TitleBarRegion.Visibility = Visibility.Collapsed;
                CommandPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                TitleBarRegion.Visibility = Visibility.Visible;
                CommandPanel.Visibility = Visibility.Visible;
            }
        };


        WebView.Focus(FocusState.Programmatic);
    }

    private void FullScreen_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        var view = ApplicationView.GetForCurrentView();
        if (view.IsFullScreenMode)
        {
            view.ExitFullScreenMode();
        }
        else
        {
            view.TryEnterFullScreenMode();
        }
    }

    private void TouchModeToggle_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        WebView.CoreWebView2.Settings.UserAgent = mobileUserAgent;
        WebView.CoreWebView2.Reload();
    }

    private void TouchModeToggle_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        WebView.CoreWebView2.Settings.UserAgent = desktopUserAgent;
        WebView.CoreWebView2.Reload();
    }
}
