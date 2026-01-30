using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoAlbum.Core.Interfaces;
using PhotoAlbum.Core.Services;
using PhotoAlbum.App.ViewModels;

namespace PhotoAlbum.App;

public partial class App : Application
{
    private static IHost? _host;
    
    public static IHost Host => _host ?? throw new InvalidOperationException("Host not initialized");

    public App()
    {
        InitializeComponent();
        
        _host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register services
                services.AddSingleton<IFaceDetectionService, FaceDetectionService>();
                services.AddSingleton<IProjectService, ProjectService>();
                services.AddSingleton<ILayoutService, LayoutService>();
                services.AddSingleton<IPhotoService, PhotoService>();
                services.AddSingleton<IAutoDesignService, AutoDesignService>();
                
                // Register ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<DesignCanvasViewModel>();
                services.AddTransient<ImageWellViewModel>();
                services.AddTransient<PlannerViewModel>();
            })
            .Build();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

    private Window? m_window;
}
