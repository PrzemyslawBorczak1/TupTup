using SQLite;
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
        await _connection.CreateTableAsync<LabelType>();
        await _connection.CreateTableAsync<TimestampLabel>();
        await _connection.CreateTableAsync<RecordingGroup>();
        await _connection.CreateTableAsync<SensorType>();
        await _connection.CreateTableAsync<SensorReading>();

        _initialized = true;
    }

    
}

