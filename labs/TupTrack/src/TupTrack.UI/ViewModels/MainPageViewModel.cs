using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TupTrack.Domain;


namespace TupTrack.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private TupState tupState = TupState.Flat;


        public ObservableCollection<string> Rooms { get; } = new ObservableCollection<string>();
    }
}
