using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TupTrack.Domain;
using TupTrack.UseCases.DTOs;
using TupTrack.UseCases.Handlers;

namespace TupTrack.UI.StatisticsPage
{
    public partial class StatisticsPageViewModel : ObservableObject
    {
        GetRecordingsSummaryHandler _handler;
        public ObservableCollection<RecordingSummaryDTO> Recordings { get; } = new();

        public StatisticsPageViewModel(GetRecordingsSummaryHandler handler)
        {
            _handler = handler;
        }

        public async void LoadSummaries()
        {
            var dbRecordings = await _handler.Handle(); 
            foreach(var r in dbRecordings)
            {
                Recordings.Add(r);
            }
           
        }
    }
}