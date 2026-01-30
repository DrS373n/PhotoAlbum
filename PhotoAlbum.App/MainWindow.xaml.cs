using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.App.ViewModels;

namespace PhotoAlbum.App;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        ViewModel = App.Host.Services.GetRequiredService<MainViewModel>();
        Title = "PhotoAlbum - Wedding Album Designer";
    }
}
