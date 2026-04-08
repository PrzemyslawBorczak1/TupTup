using System.Collections.ObjectModel;
using TupTrack.UI.Components;

namespace TupTrack.UI;

public partial class MainPage : ContentPage
{
    bool isRecording = false;
    public bool IsRecording
    {
        get => isRecording;
        set
        {
            if (isRecording == value) return;
            isRecording = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<LabelsBar.LabelSegment> RecordingLabels { get; } = new()
{
    new() { Value = 0.5, Color = Color.FromArgb("#1F5EFF") },
    new() { Value = 0.5, Color = Color.FromArgb("#6A5638") },
    new() { Value = 0.5, Color = Color.FromArgb("#1F5EFF") },
    new() { Value = 0.6767, Color = Color.FromArgb("#D91CC8") }
};


    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OnStartRecordingClicked(object? sender, EventArgs e)
    {
        IsRecording = !IsRecording;
        var last = RecordingLabelsBar.Labels.LastOrDefault();
        if (last == null) return;
        last.Value += 10;
        RecordingLabelsBar.Refresh();
    }
}   