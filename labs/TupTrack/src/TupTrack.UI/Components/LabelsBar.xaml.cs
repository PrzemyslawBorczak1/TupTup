using System.Collections.ObjectModel;

namespace TupTrack.UI.Components;

public partial class LabelsBar : ContentView
{
    public LabelsBar()
    {
        InitializeComponent();
    }

    // bindable properties from definition
    public static readonly BindableProperty LabelsProperty =
        BindableProperty.Create(
            nameof(Labels),
            typeof(ObservableCollection<LabelSegment>),
            typeof(LabelsBar),
            null,
            propertyChanged: OnLabelsChanged);

    public static readonly BindableProperty BarHeightProperty =
        BindableProperty.Create(
            nameof(BarHeight),
            typeof(double),
            typeof(LabelsBar),
            6.0);

    public ObservableCollection<LabelSegment>? Labels
    {
        get => (ObservableCollection<LabelSegment>?)GetValue(LabelsProperty);
        set => SetValue(LabelsProperty, value);
    }

    public double BarHeight
    {
        get => (double)GetValue(BarHeightProperty);
        set => SetValue(BarHeightProperty, value);
    }

    private static void OnLabelsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LabelsBar)bindable;
        control.RebuildBar();
    }

    public void Refresh()
    {
        RebuildBar();
    }

    private void RebuildBar()
    {
        BarGrid.ColumnDefinitions.Clear();
        BarGrid.Children.Clear();

        if (Labels is null || Labels.Count == 0)
            return;

        var validLabels = Labels.Where(label => label.Value > 0).ToList();
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

    public class LabelSegment
    {
        public double Value { get; set; }
        public Color Color { get; set; } = Colors.Gray;
    }
}