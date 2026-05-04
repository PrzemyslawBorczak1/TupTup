
using TupTrack.Domain;
using TupTrack.UseCases.SensorCoordinator;

namespace TupTrack.UseCases.DTOs
{
    public class StartRecordingDTO
    {
        public DateTime StartTime { get; set; }
        public TupState FirstTupState { get; set; }
        public string? Room { get; set; } 
        public SensorSpeed SensorSpeed { get; set; }

    }
}
