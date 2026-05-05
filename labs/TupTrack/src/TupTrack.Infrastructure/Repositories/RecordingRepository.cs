using System.Data.Common;
using TupTrack.Domain;
using TupTrack.UseCases.Repositories;
using Entities = TupTrack.Domain.Entities;
using Tables = TupTrack.Infrastructure.Tables;

namespace TupTrack.Infrastructure.Repositories
{
    public class RecordingRepository : IRecordingRepository
    {
        private readonly DatabaseContext _databaseContext;

        public RecordingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public async Task AddInitialRecording(Entities.Recording recording, Entities.TupStateEntity tupStateEntity, Entities.RoomTimestamp roomTimestamp)
        {
            await _databaseContext.InitAsync();

            Tables.Recording rec = new()
            {
                Id = recording.Id,
                GroupType = recording.GroupType,
                StartTime = recording.StartTime,
                EndTime = recording.EndTime,
                Note = recording.Note,
                State = recording.State
            };
            Tables.TupStateEntity tupState = new()
            {
                Id = tupStateEntity.Id,
                RecordingId = tupStateEntity.RecordingId,
                State = tupStateEntity.State,
                FromTimestamp = tupStateEntity.FromTimestamp,
                Description = tupStateEntity.Description
            };
            Tables.RoomTimestamp roomT = new ()
            {
                Id = roomTimestamp.Id,
                RoomName = roomTimestamp.RoomName,
                RecordingId = roomTimestamp.RecordingId,
                Timestamp = roomTimestamp.Timestamp,
                Description = roomTimestamp.Description,
            };


            await _databaseContext.Connection.RunInTransactionAsync(connection =>
            {
                connection.Insert(rec);
                connection.Insert(tupState);
                connection.Insert(roomT);
            });

        }

        public async Task MarkAsFailed(Guid recordingId, string? failureReason = null)
        {
            await _databaseContext.InitAsync();
            var recording = await _databaseContext.Connection.GetAsync<Tables.Recording>(recordingId);
            if (recording != null)
            {
                if(string.IsNullOrEmpty(recording.Note))
                {
                    recording.Note = $"Failed: {failureReason}";
                }
                else
                {
                    recording.Note = recording.Note + $"\nFailed: {failureReason}";
                }
                recording.State = RecordingState.Failed;
                await _databaseContext.Connection.UpdateAsync(recording);
            }
        }

       


        public async Task<Entities.Room> GetRoomAsync(string roomName)
        {
            await _databaseContext.InitAsync();
            var room = await _databaseContext.Connection.GetAsync<Tables.Room>(roomName);
            return new Entities.Room(room.Name, room.Description);
        }

    }
}
