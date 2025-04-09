using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Core;

namespace CloudGenshin;

public sealed partial class MainWindow : Window
{
    private string desktopUserAgent = "";
    private string mobileUserAgent = "";

    public bool IsFullScreen() => AppWindow.Presenter.Kind == Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen;

    public MainWindow()
    {
        InitializeComponent();

        this.ExtendsContentIntoTitleBar = true;
        this.SetTitleBar(TitleBarRegion);

        WebView.EnsureCoreWebView2Async().AsTask().ContinueWith(task =>
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                desktopUserAgent = (WebView.CoreWebView2.Settings.UserAgent);
                mobileUserAgent = desktopUserAgent.Replace("Windows NT 10.0;", "Linux; Android 12.0;").Replace("Safari/", "Mobile Safari/");


                WebView.CoreWebView2.ContainsFullScreenElementChanged += (s, e) =>
                {
                    if (s.ContainsFullScreenElement)
                    {
                        AppWindow.SetPresenter(Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen);
                    }
                    else
                    {
                        AppWindow.SetPresenter(Microsoft.UI.Windowing.AppWindowPresenterKind.Default);
                    }
                };
            });
        });

        AppWindow.Changed += (s, e) =>
        {
            if (AppWindow.Presenter.Kind == Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen)
            {
                try
                {
                    this.ExtendsContentIntoTitleBar = false;
                    this.SetTitleBar(null);
                } catch { }
                TitleBarRegion.Visibility = Visibility.Collapsed;
                CommandPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                try
                {
                    this.ExtendsContentIntoTitleBar = true;
                    this.SetTitleBar(TitleBarRegion);
                } catch { }
                
                TitleBarRegion.Visibility = Visibility.Visible;
                CommandPanel.Visibility = Visibility.Visible;
            }
        };


        WebView.Focus(FocusState.Programmatic);
    }
    
    private void TouchModeToggle_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        WebView.CoreWebView2.Settings.UserAgent = mobileUserAgent;
        WebView.CoreWebView2.Reload();
    }

    private void TouchModeToggle_Unchecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        WebView.CoreWebView2.Settings.UserAgent = desktopUserAgent;
        WebView.CoreWebView2.Reload();
    }

}
