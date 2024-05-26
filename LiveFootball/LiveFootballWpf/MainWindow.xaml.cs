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
        var chmFilePath = @"LiveFootball.chm";
        try
        {
            Process.Start("hh.exe", chmFilePath);
        }
        catch (Exception)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "An error occured at help file opening" }
            };

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