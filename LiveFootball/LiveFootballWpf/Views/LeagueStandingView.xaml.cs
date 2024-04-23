using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;
using System.Windows.Controls;

namespace LiveFootballWpf.Views;

/// <summary>
///     Interaction logic for LeagueStandingView.xaml
/// </summary>
public partial class LeagueStandingView : UserControl
{
    public LeagueStandingView()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetRequiredService<LeagueStandingViewModel>();
    }
}