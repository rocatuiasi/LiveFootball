using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;

namespace LiveFootballWpf.Views
{
    /// <summary>
    /// Interaction logic for FixturesView.xaml
    /// </summary>
    public partial class FixturesView : UserControl
    {
        public FixturesView()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<FixturesViewModel>();
        }
    }
}
