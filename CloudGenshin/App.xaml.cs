using System;
using Microsoft.UI.Xaml;

namespace CloudGenshin;

public sealed partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        //ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Colors.Transparent;
        //ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
        //ApplicationView.GetForCurrentView().TitleBar.InactiveBackgroundColor = Colors.Transparent;
        //ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        //CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

        var window = new MainWindow();
        window.Activate();        
    }
}
