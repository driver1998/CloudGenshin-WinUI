using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;

namespace CloudGenshin;

public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Colors.Transparent;
        ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
        ApplicationView.GetForCurrentView().TitleBar.InactiveBackgroundColor = Colors.Transparent;
        ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

        Window.Current.Content = new MainPage();
        Window.Current.Activate();
    }
}
