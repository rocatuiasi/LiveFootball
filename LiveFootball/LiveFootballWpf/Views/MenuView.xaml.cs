using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;

namespace LiveFootballWpf.Views;

/// <summary>
///     Interaction logic for MenuView.xaml
/// </summary>
public partial class MenuView : UserControl
{
    public MenuView()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<MenuViewModel>();
    }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (SearchTextBox.Visibility == Visibility.Visible)
        {
            SearchTextBox.Visibility = Visibility.Collapsed;
            SearchTextBox.Text = string.Empty;
            SearchButton.Content = Resources["SearchIcon"];
        }
        else
        {
            SearchTextBox.Visibility = Visibility.Visible;
            SearchButton.Content = Resources["CloseIcon"];
            SearchTextBox.Focus();
        }
    }
}