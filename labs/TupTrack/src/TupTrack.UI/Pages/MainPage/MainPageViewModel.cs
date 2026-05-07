using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TupTrack.Domain;
using TupTrack.UseCases.DTOs;
using TupTrack.UseCases.Handlers;


namespace TupTrack.UI.Pages.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        private StartRecordingHandler _startRecordingHandler;
        private GetRecordingOptionsHandler _getRecordingOptionsHandler;

        [ObservableProperty]
        private TupState tupState = TupState.Flat;

        [ObservableProperty]
        private string chosenRoom = "";

        [ObservableProperty]
        private Domain.SensorSpeed chosenSpeedSensor = Domain.SensorSpeed.Fast;

        [ObservableProperty]
        private string chosenGroup = "";


        [ObservableProperty]
        private string note = "Note";


        public ObservableCollection<string> Rooms { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<Domain.SensorSpeed> SpeedSensor { get; } = new ObservableCollection<Domain.SensorSpeed>
        {
            Domain.SensorSpeed.Default,
            Domain.SensorSpeed.Slow,
            Domain.SensorSpeed.Medium,
            Domain.SensorSpeed.Fast,
        };
        public ObservableCollection<string> Groups { get; private set; } = new ObservableCollection<string>();

        [RelayCommand]
        private void SetTupState(TupState state) => TupState = state;


        partial void OnChosenRoomChanged(string value)
        {
            if (value is null)
            {
                return;
            }
            Debug.WriteLine(chosenRoom);
        }

        partial void OnChosenSpeedSensorChanged(Domain.SensorSpeed value)
        {
            Debug.WriteLine(ChosenSpeedSensor);
        }

        partial void OnChosenGroupChanged(string value)
        {
            Debug.WriteLine(ChosenGroup);
        }



        public MainPageViewModel(StartRecordingHandler startRecordingHandler, GetRecordingOptionsHandler getRecordingOptionsHandler)
        {
            _startRecordingHandler = startRecordingHandler;
            _getRecordingOptionsHandler = getRecordingOptionsHandler;
        }

        public async Task LoadOptions()
        {
            var options = await _getRecordingOptionsHandler.Handle();

            foreach (var s in options.Rooms)
            {
                Rooms.Add(s);
            }


            foreach (var s in options.Groups)
            {
                Groups.Add(s);
            }


        }



        [RelayCommand]
        public async Task StartRecording()
        {
            try
            {

                await _startRecordingHandler.Handle(new StartRecordingDTO
                {
                    FirstTupState = TupState,
                    StartTime = DateTime.Now,
                    Room = ChosenRoom,
                    SensorSpeed = ChosenSpeedSensor,
                    GroupName = ChosenGroup,

                });
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlertAsync(
                    "Error",
                    $"Error starting recording: {ex.Message}",
                    "OK");
            }
        }



    }
}
