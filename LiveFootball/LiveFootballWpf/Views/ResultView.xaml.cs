using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;
using LiveFootball.ViewModels;

namespace LiveFootballWpf.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        public ResultView()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<ResultViewModel>();
        }
    }
}