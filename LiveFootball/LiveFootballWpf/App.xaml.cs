using System.Windows;
using ApiFootballDeserializer;
using CommunityToolkit.Mvvm.DependencyInjection;
using LiveFootball.Core.Services;
using LiveFootball.Core.ViewModels;
using LiveFootballWpf.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LiveFootballWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureServices();
            Ioc.Default.GetRequiredService<MainViewModel>().InitializeApp();
        }

        private void ConfigureServices()
        {
            // Register services
            Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                        .AddTransient<IFootballApiService, FootballApiService>() // Services
                        .AddTransient<IDeserializationService, DeserializationService>() 
                        .AddSingleton<MainViewModel>() // ViewModels
                        .AddSingleton<MenuViewModel>()
                        .AddSingleton<LeagueTabViewModel>()
                        .AddSingleton<AllGamesTabViewModel>()
                        .AddSingleton<LiveGamesViewModel>()
                        .AddSingleton<ResultsViewModel>()
                        .AddSingleton<FixturesViewModel>() 
                        .AddSingleton<LeagueStandingViewModel>()
                        .AddTransient<IDeserializerFactory, DeserializerFactory>() // Deserializer
                        .BuildServiceProvider());
        }
    }
}
