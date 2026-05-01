using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using TupTrack.Domain;

namespace TupTrack.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private TupState tupState = TupState.Flat;

        
        public ObservableCollection<string> Rooms { get; } = new ObservableCollection<string> { 
            "Room1",
            "Room2",
            "Room3",
            "Room4",
        };

        [ObservableProperty]
        private string choosenRoom = "";



        [RelayCommand]
        private void SetTupState(TupState state)
        {
            TupState = state;
        }
    }
}
