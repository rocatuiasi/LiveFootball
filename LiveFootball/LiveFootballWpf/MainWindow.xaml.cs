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
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        // Set the DataContext to the MainViewModel
        DataContext = Ioc.Default.GetRequiredService<MainViewModel>();
    }

    /// <summary>
    /// Handles the Click event of the MenuDarkModeButton control.
    /// Modifies the theme of the application based on the checked state of the button.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        => ModifyTheme(DarkModeToggleButton.IsChecked == true);

    /// <summary>
    /// Modifies the theme of the application based on the specified theme.
    /// </summary>
    /// <param name="isDarkTheme">Indicates whether to apply the dark theme.</param>
    private static void ModifyTheme(bool isDarkTheme)
    {
        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();

        theme.SetBaseTheme(isDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
        paletteHelper.SetTheme(theme);
    }

    /// <summary>
    /// Handles the Click event of the MenuHelpButton control.
    /// Opens the help file when clicked.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void MenuHelpButton_OnClick(object sender, RoutedEventArgs e)
    {
        var chmFilePath = @"LiveFootball.chm";
        try
        {
            // Open the help file
            Process.Start("hh.exe", chmFilePath);
        }
        catch (Exception)
        {
            // Display error message if unable to open help file
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "An error occurred while opening the help file." }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }
    }

    /// <summary>
    /// Handles the Click event of the MenuAboutButton control.
    /// Displays information about the application when clicked.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void MenuAboutButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Display information about the application
        var sampleMessageDialog = new SampleMessageDialog
        {
            Message = { Text = "LiveFootball Application\r\nViewing details about matches around the world\r\n(c) 2024, LiveFootball Team" }
        };

        await DialogHost.Show(sampleMessageDialog, "RootDialog");
    }
}
