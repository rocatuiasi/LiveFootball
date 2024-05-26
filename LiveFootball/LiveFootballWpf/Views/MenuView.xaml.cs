/**************************************************************************
 *                                                                        * 
 *  File:        MenuView.xaml.cs                                         *
 *  Description: LiveFootballWpf Library                                  *
 *               Code-behind file for the menu view in the application.   *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
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
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuView"/> class.
    /// </summary>
    public MenuView()
    {
        InitializeComponent();

        // Set the DataContext to the MenuViewModel
        DataContext = Ioc.Default.GetRequiredService<MenuViewModel>();
    }

    /// <summary>
    /// Handles the Click event of the SearchButton control.
    /// Toggles the visibility of the search text box and updates the button content accordingly.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (SearchTextBox.Visibility == Visibility.Visible)
        {
            // Hide the search text box
            SearchTextBox.Visibility = Visibility.Collapsed;
            SearchTextBox.Text = string.Empty;
            SearchButton.Content = Resources["SearchIcon"];
        }
        else
        {
            // Show the search text box
            SearchTextBox.Visibility = Visibility.Visible;
            SearchButton.Content = Resources["CloseIcon"];
            SearchTextBox.Focus();
        }
    }
}
