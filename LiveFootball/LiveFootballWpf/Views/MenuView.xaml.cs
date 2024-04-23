using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;

namespace LiveFootballWpf.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<MenuViewModel>();
        }
    }
}
