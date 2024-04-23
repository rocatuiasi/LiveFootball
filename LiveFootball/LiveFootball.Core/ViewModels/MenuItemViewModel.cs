using System.Windows.Input;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using LiveFootball.Core.Services;

namespace LiveFootball.Core.ViewModels;

public class MenuItemViewModel
{
    #region Backing Fields and Properties

    private readonly IFootballApiService _footballService;

    public string Name { get; set; }

    #endregion


    public ICommand FetchDataCommand => new AsyncRelayCommand(FetchData);


    #region Constructors

    public MenuItemViewModel(string name)
    {
        _footballService = Ioc.Default.GetRequiredService<IFootballApiService>();

        Name = name;
    }

    #endregion

    private async Task FetchData()
    {
        var data = await _footballService.GetStandingDataAsync();
    }
}