using TupTrack.Domain.Entities;

namespace TupTrack.UseCases.Repositories
{
    public interface IRecordingRepository
    {
        public Task AddInitialRecording(Recording recording, TupStateEntity tupStateEntity, RoomTimestamp roomTimestamp);

        public  Task MarkAsFailed(Guid recordingId, string? failureReason);

        public Task<Room> GetRoomAsync(string roomName);

    }
}
