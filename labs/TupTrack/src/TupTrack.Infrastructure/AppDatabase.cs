using SQLite;
using TupTrack.Domain;
using Entities = TupTrack.Domain.Entities;
using Tables = TupTrack.Infrastructure.Tables;

namespace TupTrack.Infrastructure;

public class AppDatabase
{
    private SQLiteAsyncConnection _connection;
    private bool _initialized = false;

    public AppDatabase(string path)
    {
        _connection = new SQLiteAsyncConnection(path);

    }

    private async Task InitAsync()
    {
        if (_initialized)
            return;

        await _connection.CreateTableAsync<Tables.Recording>();
        await _connection.CreateTableAsync<Tables.TupStateEntity>();
        await _connection.CreateTableAsync<Tables.RecordingGroup>();
        await _connection.CreateTableAsync<Tables.SensorType>();
        await _connection.CreateTableAsync<Tables.SensorReading>();
        _initialized = true;
    }


    public async Task AddRecording(Entities.Recording recording)
    {
        await InitAsync();
        Tables.Recording rec = new()
        {
            Id = recording.Id,
            GroupType = recording.GroupType,
            StartTime = recording.StartTime,
            EndTime = recording.StartTime,
            Note = recording.Note
        };
        await _connection.InsertAsync(rec);
    }

    public async Task AddTupState(Entities.TupStateEntity tupStateEntity)
    {
        await InitAsync();
        Tables.TupStateEntity tupState = new()
        {
            Id = tupStateEntity.Id,
            RecordingId = tupStateEntity.RecordingId,
            State = tupStateEntity.State,
            FromTimestamp = tupStateEntity.FromTimestamp,
            Description = tupStateEntity.Description
        };
        await _connection.InsertAsync(tupState);

    }
}

