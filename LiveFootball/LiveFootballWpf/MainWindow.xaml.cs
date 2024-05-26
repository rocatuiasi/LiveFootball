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
        //TODO: Open .chm file
    }

    private async void MenuAboutButton_OnClick(object sender, RoutedEventArgs e)
    {
        var sampleMessageDialog = new SampleMessageDialog
        {
            Message = { Text = "Despre LiveFootball, aplicatie facuta la disciplina IP" }
        };

        await DialogHost.Show(sampleMessageDialog, "RootDialog");
    }
}