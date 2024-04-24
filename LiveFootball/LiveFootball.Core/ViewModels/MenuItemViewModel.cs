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


    #region ICommand

    public ICommand FetchDataCommand => new AsyncRelayCommand(FetchData);

    #endregion


    #region Constructors

    public MenuItemViewModel(string name, IFootballApiService? footballApiService = null)
    {
        _footballService = footballApiService ?? Ioc.Default.GetRequiredService<IFootballApiService>();

        Name = name;
    }

    #endregion

    #region ICommand Execution 

    private async Task FetchData()
    {
        var data = await _footballService.GetStandingDataAsync();
    }

    #endregion
}