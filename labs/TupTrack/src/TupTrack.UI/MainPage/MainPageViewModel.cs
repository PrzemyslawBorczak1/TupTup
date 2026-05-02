using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TupTrack.Domain;
using TupTrack.Infrastructure;

namespace TupTrack.UI.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        private AppDatabase _appDb;

        [ObservableProperty]
        private TupState tupState = TupState.Flat;

        [ObservableProperty]
        private string choosenRoom = "";

        [ObservableProperty]
        private SensorSpeed choosenSpeedSensor = SensorSpeed.Default;

        [ObservableProperty]
        private string choosenGroup = "";


        [ObservableProperty]
        private string note = "Note";


        public ObservableCollection<string> Rooms { get; } = new ObservableCollection<string>();
        public ObservableCollection<SensorSpeed> SpeedSensor { get; } = new ObservableCollection<SensorSpeed>
        {
            SensorSpeed.Default,
            SensorSpeed.UI,
            SensorSpeed.Game,
            SensorSpeed.Fastest,
        };
        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>();

        [RelayCommand]
        private void SetTupState(TupState state) => TupState = state;


        partial void OnChoosenRoomChanged(string value)
        {
            if (value is null)
            {
                return;
            }
            Debug.WriteLine(choosenRoom);
        }

        partial void OnChoosenSpeedSensorChanged(SensorSpeed value)
        {
            Debug.WriteLine(ChoosenSpeedSensor);
        }

        partial void OnChoosenGroupChanged(string value)
        {
            Debug.WriteLine(ChoosenGroup);
        }



        public MainPageViewModel(AppDatabase appDatabase)
        {
            _appDb = appDatabase;
        }

        public async Task LoadOptions()
        {
            var rooms = new ObservableCollection<string>
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

            var groups = new ObservableCollection<string>
                {
                    "Gr1",
                    "Gr2",
                    "Gr3",
                    "Gr4",
                    "Gr5",
                };


        }

    }
}
