using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TupTrack.Domain;

namespace TupTrack.UI.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private TupState tupState = TupState.Flat;


        [RelayCommand]
        private void SetTupState(TupState state)
        {
            TupState = state;
        }

        public ObservableCollection<string> Rooms { get; } = new ObservableCollection<string> { 
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

        [ObservableProperty]
        private string choosenRoom = "";

        partial void OnChoosenRoomChanged(string? value)
        {
            if (value is null)
            {
                return;
            }
            Debug.WriteLine(choosenRoom);
        }


        public ObservableCollection<SensorSpeed> SpeedSensor { get; } = new ObservableCollection<SensorSpeed>
        {
            SensorSpeed.Default,
            SensorSpeed.UI,
            SensorSpeed.Game,
            SensorSpeed.Fastest,
        };

        [ObservableProperty]
        private SensorSpeed choosenSpeedSensor = SensorSpeed.Default;

        partial void OnChoosenSpeedSensorChanged(SensorSpeed value)
        {
            Debug.WriteLine(ChoosenSpeedSensor);
        }


        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>
        {
            "Gr1",
            "Gr2",
            "Gr3",
            "Gr4",
            "Gr5",
        };

        [ObservableProperty]
        private string choosenGroup = "";

        partial void OnChoosenGroupChanged(string value)
        {
            Debug.WriteLine(ChoosenGroup);
        }



        [ObservableProperty]
        private string note = "Note";



    }
}
