using SQLite;
using TupTrack.Domain;
using TupTrack.Infrastructure.Records;

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

        await _connection.CreateTableAsync<Recording>();
        await _connection.CreateTableAsync<TupStateEntity>();
        await _connection.CreateTableAsync<RecordingGroup>();
        await _connection.CreateTableAsync<SensorType>();
        await _connection.CreateTableAsync<SensorReading>();

        _initialized = true;
    }

    public async Task<Guid> AddStartRecordingAsync(StartRecordingDTO startRecordingDTO)
    {
        await InitAsync();

        var id = Guid.NewGuid();
        var recording = new Recording
        {
            Id = id,
            StartTime = startRecordingDTO.StartTime,
        };


        var tupStateId = Guid.NewGuid();
        var tupStateEntity = new TupStateEntity
        {
            Id = tupStateId,
            RecordingId = id,
            FromTimestamp = startRecordingDTO.StartTime,
            State = startRecordingDTO.FirstTupState,
        };


        await _connection.RunInTransactionAsync(tran =>
        {
            tran.Insert(recording);
            tran.Insert(tupStateEntity);
        });

        return id;

    }


}

