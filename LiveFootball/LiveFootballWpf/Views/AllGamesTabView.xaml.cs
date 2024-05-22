using System.Windows.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;

namespace LiveFootballWpf.Views
{
    /// <summary>
    /// Interaction logic for AllGamesTabView.xaml
    /// </summary>
    public partial class AllGamesTabView : UserControl
    {
        public AllGamesTabView()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<AllGamesTabViewModel>();
        }
    }
}
