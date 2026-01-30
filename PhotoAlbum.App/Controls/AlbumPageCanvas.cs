using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.App.Controls;

/// <summary>
/// Custom canvas control for album page design
/// </summary>
public sealed class AlbumPageCanvas : Canvas
{
    public static readonly DependencyProperty AlbumPageProperty =
        DependencyProperty.Register(
            nameof(AlbumPage),
            typeof(AlbumPage),
            typeof(AlbumPageCanvas),
            new PropertyMetadata(null, OnAlbumPageChanged));

    public AlbumPage? AlbumPage
    {
        get => (AlbumPage?)GetValue(AlbumPageProperty);
        set => SetValue(AlbumPageProperty, value);
    }

    public static readonly DependencyProperty ShowCutLinesProperty =
        DependencyProperty.Register(
            nameof(ShowCutLines),
            typeof(bool),
            typeof(AlbumPageCanvas),
            new PropertyMetadata(true, OnShowCutLinesChanged));

    public bool ShowCutLines
    {
        get => (bool)GetValue(ShowCutLinesProperty);
        set => SetValue(ShowCutLinesProperty, value);
    }

    public static readonly DependencyProperty ShowSafeZonesProperty =
        DependencyProperty.Register(
            nameof(ShowSafeZones),
            typeof(bool),
            typeof(AlbumPageCanvas),
            new PropertyMetadata(true, OnShowSafeZonesChanged));

    public bool ShowSafeZones
    {
        get => (bool)GetValue(ShowSafeZonesProperty);
        set => SetValue(ShowSafeZonesProperty, value);
    }

    public static readonly DependencyProperty PageWidthInchesProperty =
        DependencyProperty.Register(
            nameof(PageWidthInches),
            typeof(double),
            typeof(AlbumPageCanvas),
            new PropertyMetadata(12.0, OnPageSizeChanged));

    public double PageWidthInches
    {
        get => (double)GetValue(PageWidthInchesProperty);
        set => SetValue(PageWidthInchesProperty, value);
    }

    public static readonly DependencyProperty PageHeightInchesProperty =
        DependencyProperty.Register(
            nameof(PageHeightInches),
            typeof(double),
            typeof(AlbumPageCanvas),
            new PropertyMetadata(12.0, OnPageSizeChanged));

    public double PageHeightInches
    {
        get => (double)GetValue(PageHeightInchesProperty);
        set => SetValue(PageHeightInchesProperty, value);
    }

    private const double DPI = 100; // Pixels per inch for display
    private Rectangle? _cutLineRectangle;
    private Rectangle? _safeZoneRectangle;

    public AlbumPageCanvas()
    {
        Background = new SolidColorBrush(Microsoft.UI.Colors.White);
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateCanvas();
    }

    private static void OnAlbumPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AlbumPageCanvas canvas)
        {
            canvas.UpdateCanvas();
        }
    }

    private static void OnShowCutLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AlbumPageCanvas canvas)
        {
            canvas.UpdateGuides();
        }
    }

    private static void OnShowSafeZonesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AlbumPageCanvas canvas)
        {
            canvas.UpdateGuides();
        }
    }

    private static void OnPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AlbumPageCanvas canvas)
        {
            canvas.UpdateCanvas();
        }
    }

    private void UpdateCanvas()
    {
        Children.Clear();

        Width = PageWidthInches * DPI;
        Height = PageHeightInches * DPI;

        // Add background
        if (AlbumPage?.Background != null)
        {
            AddBackground();
        }

        // Add drop zones
        if (AlbumPage?.DropZones != null)
        {
            foreach (var dropZone in AlbumPage.DropZones)
            {
                AddDropZone(dropZone);
            }
        }

        // Add text elements
        if (AlbumPage?.TextElements != null)
        {
            foreach (var textElement in AlbumPage.TextElements)
            {
                AddTextElement(textElement);
            }
        }

        UpdateGuides();
    }

    private void AddBackground()
    {
        if (AlbumPage?.Background == null) return;

        var bg = AlbumPage.Background;
        var rect = new Rectangle
        {
            Width = Width,
            Height = Height,
            Fill = new SolidColorBrush(ParseColor(bg.Color)),
            Opacity = bg.Opacity
        };

        SetLeft(rect, 0);
        SetTop(rect, 0);
        SetZIndex(rect, -1);
        Children.Add(rect);
    }

    private void AddDropZone(DropZone dropZone)
    {
        var control = new DropZoneControl
        {
            DropZone = dropZone
        };

        Children.Add(control);
    }

    private void AddTextElement(TextElement textElement)
    {
        var textBlock = new TextBlock
        {
            Text = textElement.Text,
            FontSize = textElement.FontSize,
            FontFamily = new FontFamily(textElement.FontFamily),
            Foreground = new SolidColorBrush(ParseColor(textElement.Color)),
            Width = textElement.Width * DPI,
            Height = textElement.Height * DPI,
            TextWrapping = TextWrapping.Wrap
        };

        if (textElement.IsBold)
        {
            textBlock.FontWeight = Microsoft.UI.Text.FontWeights.Bold;
        }

        if (textElement.IsItalic)
        {
            textBlock.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }

        textBlock.TextAlignment = textElement.Alignment switch
        {
            Core.Models.TextAlignment.Center => Microsoft.UI.Xaml.TextAlignment.Center,
            Core.Models.TextAlignment.Right => Microsoft.UI.Xaml.TextAlignment.Right,
            Core.Models.TextAlignment.Justify => Microsoft.UI.Xaml.TextAlignment.Justify,
            _ => Microsoft.UI.Xaml.TextAlignment.Left
        };

        SetLeft(textBlock, textElement.X * DPI);
        SetTop(textBlock, textElement.Y * DPI);
        
        if (textElement.Rotation != 0)
        {
            textBlock.RenderTransform = new RotateTransform { Angle = textElement.Rotation };
        }

        Children.Add(textBlock);
    }

    private void UpdateGuides()
    {
        // Remove existing guides
        if (_cutLineRectangle != null) Children.Remove(_cutLineRectangle);
        if (_safeZoneRectangle != null) Children.Remove(_safeZoneRectangle);

        if (ShowCutLines)
        {
            _cutLineRectangle = new Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = new SolidColorBrush(Microsoft.UI.Colors.Red),
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 5, 3 },
                Opacity = 0.5
            };
            SetLeft(_cutLineRectangle, 0);
            SetTop(_cutLineRectangle, 0);
            SetZIndex(_cutLineRectangle, 1000);
            Children.Add(_cutLineRectangle);
        }

        if (ShowSafeZones)
        {
            const double safeZone = 0.25 * DPI; // 0.25 inches
            _safeZoneRectangle = new Rectangle
            {
                Width = Width - (safeZone * 2),
                Height = Height - (safeZone * 2),
                Stroke = new SolidColorBrush(Microsoft.UI.Colors.Blue),
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 3, 3 },
                Opacity = 0.5
            };
            SetLeft(_safeZoneRectangle, safeZone);
            SetTop(_safeZoneRectangle, safeZone);
            SetZIndex(_safeZoneRectangle, 1000);
            Children.Add(_safeZoneRectangle);
        }
    }

    private static Windows.UI.Color ParseColor(string colorString)
    {
        try
        {
            if (colorString.StartsWith("#"))
            {
                colorString = colorString.TrimStart('#');
                
                if (colorString.Length == 6)
                {
                    return Windows.UI.Color.FromArgb(
                        255,
                        Convert.ToByte(colorString.Substring(0, 2), 16),
                        Convert.ToByte(colorString.Substring(2, 2), 16),
                        Convert.ToByte(colorString.Substring(4, 2), 16)
                    );
                }
            }
        }
        catch
        {
            // Return white on parse error
        }

        return Windows.UI.Colors.White;
    }
}
