using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TupTrack.Domain;
using TupTrack.UseCases.DTOs;

namespace TupTrack.UI.StatisticsPage
{
    public partial class StatisticsPageViewModel : ObservableObject
    {
        public ObservableCollection<RecordingSummaryDTO> Recordings { get; } = new();

        public StatisticsPageViewModel()
        {
            // Seed data for now
            Recordings.Add(new RecordingSummaryDTO
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddMinutes(-25),
                EndTime = DateTime.Now.AddMinutes(-5),
                State = RecordingState.Completed,
                GroupName = "Living room"
            });

            Recordings.Add(new RecordingSummaryDTO
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddHours(-3),
                EndTime = null,
                State = RecordingState.Ongoing,
                GroupName = "Bedroom"
            });

            Recordings.Add(new RecordingSummaryDTO
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(-1).AddMinutes(18),
                State = RecordingState.Failed,
                GroupName = "Kitchen"
            });
        }
    }
}