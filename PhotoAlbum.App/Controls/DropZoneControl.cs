using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.App.Controls;

/// <summary>
/// Custom control for rendering a drop zone on the design canvas
/// </summary>
public sealed class DropZoneControl : ContentControl
{
    public static readonly DependencyProperty DropZoneProperty =
        DependencyProperty.Register(
            nameof(DropZone),
            typeof(DropZone),
            typeof(DropZoneControl),
            new PropertyMetadata(null, OnDropZoneChanged));

    public DropZone? DropZone
    {
        get => (DropZone?)GetValue(DropZoneProperty);
        set => SetValue(DropZoneProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(DropZoneControl),
            new PropertyMetadata(false, OnIsSelectedChanged));

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    private Border? _border;
    private Image? _photoImage;

    public DropZoneControl()
    {
        DefaultStyleKey = typeof(DropZoneControl);
        
        PointerEntered += OnPointerEntered;
        PointerExited += OnPointerExited;
        PointerPressed += OnPointerPressed;
        
        AllowDrop = true;
        DragEnter += OnDragEnter;
        DragOver += OnDragOver;
        Drop += OnDrop;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _border = GetTemplateChild("PART_Border") as Border;
        _photoImage = GetTemplateChild("PART_PhotoImage") as Image;
        UpdateVisuals();
    }

    private static void OnDropZoneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DropZoneControl control)
        {
            control.UpdateVisuals();
        }
    }

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DropZoneControl control)
        {
            control.UpdateBorderStyle();
        }
    }

    private void UpdateVisuals()
    {
        if (DropZone == null || _border == null) return;

        // Update size and position
        Width = DropZone.Width * 100; // Convert inches to pixels (100 DPI)
        Height = DropZone.Height * 100;
        Canvas.SetLeft(this, DropZone.X * 100);
        Canvas.SetTop(this, DropZone.Y * 100);

        // Update rotation
        var transform = new RotateTransform { Angle = DropZone.Rotation };
        RenderTransform = transform;

        // Update photo if present
        if (_photoImage != null && DropZone.Photo != null)
        {
            // In real implementation, load the actual image
            _photoImage.Visibility = Visibility.Visible;
        }
        else if (_photoImage != null)
        {
            _photoImage.Visibility = Visibility.Collapsed;
        }

        UpdateBorderStyle();
    }

    private void UpdateBorderStyle()
    {
        if (_border == null) return;

        if (IsSelected)
        {
            _border.BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Blue);
            _border.BorderThickness = new Thickness(3);
        }
        else
        {
            _border.BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Gray);
            _border.BorderThickness = new Thickness(1);
        }
    }

    private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
    {
        if (_border != null && !IsSelected)
        {
            _border.BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.LightBlue);
            _border.BorderThickness = new Thickness(2);
        }
    }

    private void OnPointerExited(object sender, PointerRoutedEventArgs e)
    {
        if (!IsSelected)
        {
            UpdateBorderStyle();
        }
    }

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        IsSelected = true;
        e.Handled = true;
    }

    private void OnDragEnter(object sender, DragEventArgs e)
    {
        e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
    }

    private void OnDragOver(object sender, DragEventArgs e)
    {
        e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
    }

    private async void OnDrop(object sender, DragEventArgs e)
    {
        if (e.DataView.Contains(Windows.ApplicationModel.DataTransfer.StandardDataFormats.StorageItems))
        {
            var items = await e.DataView.GetStorageItemsAsync();
            // Handle photo drop - would update DropZone.Photo
        }
    }
}
