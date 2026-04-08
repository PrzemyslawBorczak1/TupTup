using System.Collections.Generic;

namespace TupTrack.UI.Components;
/// <summary>
/// It binds to Height and Labels, where Labels is a collection of LabelSegment, which has Value and Color. It builds a bar with segments based on those values and colors.
/// To Upadate the bar, you can call Refresh() method, which will rebuild the bar based on the current Labels collection. You can also set BarHeight to change the height of the bar.
/// </summary>
public partial class LabelsBar : ContentView
{
    private readonly List<LabelSegment> _segments = [];

    public class LabelSegment
    {
        public double Value { get; set; }
        public Color Color { get; set; } = Colors.Gray;
    }


    public LabelsBar()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty BarHeightProperty =
        BindableProperty.Create(
            nameof(BarHeight),
            typeof(double),
            typeof(LabelsBar),
            6.0);

    public double BarHeight
    {
        get => (double)GetValue(BarHeightProperty);
        set => SetValue(BarHeightProperty, value);
    }

    public void Add(LabelSegment segment)
    {
        _segments.Add(segment);
        RebuildBar();
    }

    public LabelSegment? Last()
    {
        return _segments.Count == 0 ? null : _segments[^1];
    }

    public void Refresh()
    {
        RebuildBar();
    }

    private void RebuildBar()
    {
        BarGrid.ColumnDefinitions.Clear();
        BarGrid.Children.Clear();

        if (_segments.Count == 0)
            return;

        var validLabels = _segments.Where(label => label.Value > 0).ToList();
        if (validLabels.Count == 0)
            return;

        foreach (var label in validLabels)
        {
            BarGrid.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(label.Value, GridUnitType.Star)
                });
        }

        for (int i = 0; i < validLabels.Count; i++)
        {
            var label = validLabels[i];

            var box = new BoxView
            {
                Color = label.Color,
                CornerRadius = new CornerRadius(4),
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            BarGrid.Add(box);
            Grid.SetColumn(box, i);
        }
    }

  
}