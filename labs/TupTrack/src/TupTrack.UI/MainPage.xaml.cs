using TupTrack.UI.Components;
using UC = TupTrack.UseCases;

namespace TupTrack.UI;

public partial class MainPage : ContentPage
{
    private readonly IDispatcherTimer _recordingTimer;
    private TimeSpan _recordingElapsed;
    UC.IApplication _app;

    public MainPage(UC.IApplication app)
    {
        
        InitializeComponent();

        _app = app;

        _recordingTimer = Dispatcher.CreateTimer();
        _recordingTimer.Interval = TimeSpan.FromSeconds(1);
        _recordingTimer.Tick += OnRecordingTick;

        SeedBars();
    }

    private void OnStartRecordingClicked(object? sender, EventArgs e)
    {
        _app.StartRecording();
        SetRecordingState(true);
    }

    private void OnStopRecordingClicked(object? sender, EventArgs e)
    {
        SetRecordingState(false);
    }

    private void SetRecordingState(bool isRecording)
    {
        StartRecordingButton.IsVisible = !isRecording;
        RecordingInfoView.IsVisible = isRecording;
        StopRecordingButton.Opacity = isRecording ? 1 : 0;

        if (isRecording)
        {
            _recordingElapsed = TimeSpan.Zero;
            UpdateRecordingLabel();
            _recordingTimer.Start();
            return;
        }

        _recordingTimer.Stop();
    }

    private void OnRecordingTick(object? sender, EventArgs e)
    {
        _recordingElapsed = _recordingElapsed.Add(TimeSpan.FromSeconds(1));
        UpdateRecordingLabel();
    }

    private void UpdateRecordingLabel()
    {
        RecordingTimeLabel.Text = _recordingElapsed.ToString(@"mm\:ss");
    }

    private void SeedBars()
    {
        RecordingTopBar.Add(new LabelsBar.LabelSegment { Value = 1.2, Color = Color.FromArgb("#1F5EFF") });
        RecordingTopBar.Add(new LabelsBar.LabelSegment { Value = 0.7, Color = Color.FromArgb("#6A5638") });
        RecordingTopBar.Add(new LabelsBar.LabelSegment { Value = 1.2, Color = Color.FromArgb("#1F5EFF") });
        RecordingTopBar.Add(new LabelsBar.LabelSegment { Value = 0.7, Color = Color.FromArgb("#6A5638") });
        RecordingTopBar.Add(new LabelsBar.LabelSegment { Value = 1.2, Color = Color.FromArgb("#D91CC8") });

        RecordingBottomBar.Add(new LabelsBar.LabelSegment { Value = 5.0, Color = Color.FromArgb("#6A5638") });

    }

  

}   