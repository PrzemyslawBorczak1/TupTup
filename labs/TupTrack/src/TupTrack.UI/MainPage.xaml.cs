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

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        RecordingLabelsBar.Add(new LabelsBar.LabelSegment { Value = 0.5, Color = Color.FromArgb("#1F5EFF") });
        RecordingLabelsBar.Add(new LabelsBar.LabelSegment { Value = 0.5, Color = Color.FromArgb("#6A5638") });
        RecordingLabelsBar.Add(new LabelsBar.LabelSegment { Value = 0.5, Color = Color.FromArgb("#1F5EFF") });
        RecordingLabelsBar.Add(new LabelsBar.LabelSegment { Value = 0.6767, Color = Color.FromArgb("#D91CC8") });
    }

    private void OnStartRecordingClicked(object? sender, EventArgs e)
    {
        IsRecording = !IsRecording;
        var last = RecordingLabelsBar.Last();
        if (last == null) return;
        last.Value += 10;
        RecordingLabelsBar.Refresh();
    }
}   