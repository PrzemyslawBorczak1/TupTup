using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TupTrack.Domain;
using TupTrack.UseCases.Handlers;
using TupTrack.UseCases.DTOs;

namespace TupTrack.UI.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        private StartRecordingHandler _startRecordingHandler;

        [ObservableProperty]
        private TupState tupState = TupState.Flat;

        [ObservableProperty]
        private string chosenRoom = "";

        [ObservableProperty]
        private SensorSpeed chosenSpeedSensor = SensorSpeed.Fastest;

        [ObservableProperty]
        private string chosenGroup = "";


        [ObservableProperty]
        private string note = "Note";


        public ObservableCollection<string> Rooms { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<SensorSpeed> SpeedSensor { get; } = new ObservableCollection<SensorSpeed>
        {
            SensorSpeed.Default,
            SensorSpeed.UI,
            SensorSpeed.Game,
            SensorSpeed.Fastest,
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

        partial void OnChosenSpeedSensorChanged(SensorSpeed value)
        {
            Debug.WriteLine(ChosenSpeedSensor);
        }

        partial void OnChosenGroupChanged(string value)
        {
            Debug.WriteLine(ChosenGroup);
        }



        public MainPageViewModel(StartRecordingHandler startRecordingHandler)
        {
            _startRecordingHandler = startRecordingHandler;
        }

        public async Task LoadOptions()
        {
            if (Rooms.Count == 0)
            {
                List<string> newRooms = new()
            {
                "Room1",
            "Room2",
            "Room3",
            "Room4",
            "Room2",
            "Room3",
            "Room4",
            "Room1",
            "Room2",
            "Room3",
            "Room4",
            "Room2",
            "Room3",
            "Room4",
            "Room1",
            "Room2",
            "Room3",
            "Room4",
            "Room2",
            "Room3",
            "Room4",
            "Room1",
            "Room2",
            "Room3",
            "Room4",
            "Room2",
            "Room3",
            "Room4",
            "Room67",
        };
                foreach (var s in newRooms)
                {
                    Rooms.Add(s);
                }
            }

            if (Groups.Count == 0)
            {
                List<string> newGroups = new()
                {
                    "Gr1",
                    "Gr2",
                    "Gr3",
                    "Gr4",
                    "Gr5",
                };
                foreach (var s in newGroups)
                {
                    Groups.Add(s);
                }
            }

            Debug.WriteLine("\n\n\n\n\n\nOptions loaded\n\n\n\n\n\n\n");
        }



        [RelayCommand]
        public async Task StartRecording()
            => await _startRecordingHandler.StartRecording(new StartRecordingDTO { FirstTupState = TupState, StartTime = DateTime.Now });
        


    }
}
