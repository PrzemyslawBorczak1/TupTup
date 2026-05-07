
using TupTrack.UseCases.Handlers;
using TupTrack.UseCases.DTOs;
using TupTrack.UI;

namespace TupTrack.UI.Pages.MainPage;

public partial class MainPage : ContentPage
{
    private readonly IDispatcherTimer _recordingTimer;
    private TimeSpan _recordingElapsed;

    public MainPage( MainPageViewModel vm)
    {
        
        InitializeComponent();
        BindingContext = vm;


        _recordingTimer = Dispatcher.CreateTimer();
        _recordingTimer.Interval = TimeSpan.FromSeconds(1);
        _recordingTimer.Tick += OnRecordingTick;

        SeedBars();
    }

    private async void OnAppearance(object? sender, EventArgs e)
    {
        if (BindingContext is MainPageViewModel vm) {
            await vm.LoadOptions();
        }
    }

    //private async void OnStartRecordingClicked(object? sender, EventArgs e)
    //{
        
    //    SetRecordingState(true);
    //}

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