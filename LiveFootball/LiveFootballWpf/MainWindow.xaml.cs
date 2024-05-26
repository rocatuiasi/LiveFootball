/**************************************************************************
 *                                                                        * 
 *  File:        MainWindow.xaml.cs                                       *
 *  Description: LiveFootballWpf Library                                  *
 *               Code-behind file for the main window of the application. *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using System.Diagnostics;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;
using LiveFootballWpf.Controls;
using MaterialDesignThemes.Wpf;

namespace LiveFootballWpf;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }

    private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        => ModifyTheme(DarkModeToggleButton.IsChecked == true);

    private static void ModifyTheme(bool isDarkTheme)
    {
        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();

        theme.SetBaseTheme(isDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
        paletteHelper.SetTheme(theme);
    }

    private async void MenuHelpButton_OnClick(object sender, RoutedEventArgs e)
    {   
        var chmFilePath = "Assets/Docs/LiveFootball.chm";
        var sampleMessageDialog = new SampleMessageDialog { Message = { Text = "An error occured at help file opening" } };
        
        if (!System.IO.File.Exists(chmFilePath))
        {
            await DialogHost.Show(sampleMessageDialog, "RootDialog");
            return;
        }
            
        try
        {
            Process.Start("hh.exe", chmFilePath);
        }
        catch (Exception)
        {
            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }
    }

    private async void MenuAboutButton_OnClick(object sender, RoutedEventArgs e)
    {
        var sampleMessageDialog = new SampleMessageDialog
        {
            Message = { Text = "LiveFootball Application\r\nViewing details about matches around the world\r\n(c) 2024, LiveFootball Team" }
        };

        await DialogHost.Show(sampleMessageDialog, "RootDialog");
    }
}