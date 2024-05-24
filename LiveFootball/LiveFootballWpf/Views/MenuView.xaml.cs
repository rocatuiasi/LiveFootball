using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;

namespace LiveFootballWpf.Views;

/// <summary>
///     Interaction logic for MenuView.xaml
/// </summary>
public partial class MenuView : UserControl, IDisposable
{
    #region Private Properties and Fields
    
    private CancellationTokenSource _cancellationTokenSource;

    private ReadOnlyCollection<MenuItemViewModel> OriginalLeagues { get; }

    private ObservableCollection<MenuItemViewModel> FilteredLeagues
    {
        get => (ObservableCollection<MenuItemViewModel>)LeaguesItemsControl.ItemsSource;
        set => LeaguesItemsControl.ItemsSource = value;
    }

    private bool IsLoading
    {
        set => ((MenuViewModel)DataContext).IsLoading = value;
    }

    #endregion

    public MenuView()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<MenuViewModel>();

        OriginalLeagues = new ReadOnlyCollection<MenuItemViewModel>(((MenuViewModel)DataContext).Leagues);
        FilteredLeagues = new ObservableCollection<MenuItemViewModel>(OriginalLeagues);

        _cancellationTokenSource = new CancellationTokenSource();
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

    private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Cancel any pending filtering operation
        await _cancellationTokenSource.CancelAsync();

        // Create a new cancellation token source
        _cancellationTokenSource = new CancellationTokenSource();

        IsLoading = true;
        // Perform filtering asynchronously
        await FilterAsync(SearchTextBox.Text.ToLower(), _cancellationTokenSource.Token);
        IsLoading = false;
    }

    private async Task FilterAsync(string searchText, CancellationToken cancellationToken)
    {
        try
        {
            await Dispatcher.InvokeAsync(FilteredLeagues.Clear, DispatcherPriority.Background, cancellationToken);

            var filtered = string.IsNullOrWhiteSpace(searchText)
                ? await Task.Run(() => OriginalLeagues.ToList(), cancellationToken)
                : await Task.Run(() => FilterLeagues(searchText, OriginalLeagues, cancellationToken), cancellationToken);

            await Dispatcher.InvokeAsync(() =>
            {
                FilteredLeagues = new ObservableCollection<MenuItemViewModel>(filtered);
            }, DispatcherPriority.Background, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // The operation was cancelled, no need to handle this exception
        }
    }

    private IEnumerable<MenuItemViewModel> FilterLeagues(string searchText, IEnumerable<MenuItemViewModel> leagues, CancellationToken cancellationToken)
    {
        var words = searchText.Split(' ');
        var filtered = new List<MenuItemViewModel>();

        foreach (var league in leagues)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (ContainsWords(league, words))
                filtered.Add(league);
        }

        return filtered;
    }

    private bool ContainsWords(MenuItemViewModel league, string[] words)
    {
        if (words.Length == 0)
            return true;

        var lowercaseName = (league.Name?.ToLower() ?? string.Empty).Replace(" ", "");
        int startIndex = 0;

        foreach (var word in words)
        {
            int index = lowercaseName.IndexOf(word, startIndex, StringComparison.CurrentCultureIgnoreCase);
            if (index == -1)
                return false;

            startIndex = index + word.Length;
        }

        return true;
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}