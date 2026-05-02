using TupTrack.Domain;
using Entities = TupTrack.Domain.Entities;
using Tables = TupTrack.Infrastructure.Tables;
using TupTrack.UseCases.Repositories;

namespace TupTrack.Infrastructure.Repositories
{
    public class RecordingRepository : IRecordingRepository
    {
        DatabaseContext _databaseContext;

        public RecordingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }



        public async Task AddRecording(Entities.Recording recording)
        {
            await _databaseContext.InitAsync();
            Tables.Recording rec = new()
            {
                Id = recording.Id,
                GroupType = recording.GroupType,
                StartTime = recording.StartTime,
                EndTime = recording.StartTime,
                Note = recording.Note
            };
            await _databaseContext.Connection.InsertAsync(rec);
        }

        public async Task AddTupState(Entities.TupStateEntity tupStateEntity)
        {
            await _databaseContext.InitAsync();
            Tables.TupStateEntity tupState = new()
            {
                Id = tupStateEntity.Id,
                RecordingId = tupStateEntity.RecordingId,
                State = tupStateEntity.State,
                FromTimestamp = tupStateEntity.FromTimestamp,
                Description = tupStateEntity.Description
            };
            await _databaseContext.Connection.InsertAsync(tupState);

        }
    }
}
