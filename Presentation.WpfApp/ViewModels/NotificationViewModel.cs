using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Media;

namespace Presentation.WpfApp.ViewModels;

public partial class NotificationViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;


    [ObservableProperty]
    private ObservableObject _previousViewModel;

    [ObservableProperty]
    private Brush _color = null!;

    [ObservableProperty]
    private string _notification;
    [ObservableProperty]
    private bool _success;

    public NotificationViewModel(IServiceProvider serviceProvider, string notifcation, bool succes, ObservableObject previousViewModel)
    {
        _notification = notifcation;
        _success = succes;
        _serviceProvider = serviceProvider;
        _previousViewModel = previousViewModel;
        NotificationColor();
    }


    [RelayCommand]
    private void OkBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

        mainViewModel.CurrentViewModel = Success ? _serviceProvider.GetRequiredService<MainMenuViewModel>() : PreviousViewModel;

    }

    private void NotificationColor()
    {
        Color = Success ? Brushes.Green : Brushes.Red;
    }
}
