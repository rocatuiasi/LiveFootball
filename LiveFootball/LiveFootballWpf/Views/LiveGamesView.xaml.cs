using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiveFootballWpf.Views
{
    /// <summary>
    /// Interaction logic for LiveGamesView.xaml
    /// </summary>
    public partial class LiveGamesView : UserControl
    {
        public LiveGamesView()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<LiveGamesViewModel>();

        }
    }
}
